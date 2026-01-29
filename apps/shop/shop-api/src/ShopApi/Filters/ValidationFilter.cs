using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Offgrid.ShopApi.Filters;

public class ValidationFilter<TRequest> : IEndpointFilter
{
    private const string ApplicationProblemJsonMediaType = "application/problem+json";
    private const string BadRequestProblemType = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
    private const string UnprocessableContentProblemType = "https://tools.ietf.org/html/rfc9110#name-422-unprocessable-content";

    private readonly IWebHostEnvironment _environment;
    private readonly TimeProvider _timeProvider;

    public ValidationFilter(IWebHostEnvironment environment, TimeProvider timeProvider)
    {
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<TRequest>().FirstOrDefault();

        if (request == null)
        {
            context.HttpContext.Response.ContentType = ApplicationProblemJsonMediaType;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return Results.BadRequest(CreateProblemDetails(context.HttpContext));
        }

        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            context.HttpContext.Response.ContentType = ApplicationProblemJsonMediaType;
            context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            var problem = CreateValidationProblemDetails(validationResults, context.HttpContext);
            return TypedResults.Problem(problem);
        }

        return await next(context);
    }

    private ProblemDetails CreateProblemDetails(HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Invalid Request",
            Type = BadRequestProblemType,
            Detail = "Request body cannot be null",
            Status = StatusCodes.Status400BadRequest,
            Instance = httpContext.Request.Path.Value
        };

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }

    private ValidationProblemDetails CreateValidationProblemDetails(List<ValidationResult> validationResults, HttpContext httpContext)
    {
        static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
                return input;

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        var errors = validationResults
            .GroupBy(v => v.MemberNames.FirstOrDefault() ?? "")
            .ToDictionary(
                g => ToCamelCase(g.Key),
                g => g.Select(v => v.ErrorMessage ?? "").ToArray()
            );

        var problemDetails = new ValidationProblemDetails
        {
            Title = "Validation Error",
            Detail = "One or more validation errors occurred. See the errors property for details.",
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = UnprocessableContentProblemType,
            Instance = httpContext.Request.Path.Value,
            Errors = errors
        };

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }
}
