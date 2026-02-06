using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.System;

namespace Offgrid.Framework.AspNetCore.Http.Filters;

public class ValidationFilter<TRequest> : IEndpointFilter
{
    private readonly TimeProvider _timeProvider;

    public ValidationFilter(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<TRequest>().FirstOrDefault();

        if (request == null)
        {
            context.HttpContext.Response.ContentType = ContentTypeNames.Application.ProblemDetailsJson;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return Results.BadRequest(CreateProblemDetails(context.HttpContext));
        }

        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            context.HttpContext.Response.ContentType = ContentTypeNames.Application.ProblemDetailsJson;
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
            Type = ProblemTypeNames.Status400BadRequest,
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
        var errors = validationResults
            .GroupBy(v => v.MemberNames.FirstOrDefault() ?? "")
            .ToDictionary(
                g => g.Key.ToCamelCase(),
                g => g.Select(v => v.ErrorMessage ?? "").ToArray()
            );

        var problemDetails = new ValidationProblemDetails
        {
            Title = "Validation Error",
            Detail = "One or more validation errors occurred. See the errors property for details.",
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = ProblemTypeNames.Status422UnprocessableContent,
            Instance = httpContext.Request.Path.Value,
            Errors = errors
        };

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }
}
