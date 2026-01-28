using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Offgrid.Customers.Domain.Exceptions;

namespace Offgrid.ShopApi.ExceptionHandlers;

public sealed class ValidationExceptionHandler : IExceptionHandler
{
    private const string ApplicationProblemJsonMediaType = "application/problem+json";
    private const string UnprocessableContentProblemType = "https://tools.ietf.org/html/rfc9110#name-422-unprocessable-content";

    private readonly ILogger<ValidationExceptionHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger, TimeProvider timeProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;

        var problemDetails = CreateProblemDetails(validationException, httpContext);

        await WriteProblemDetailsAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    private ValidationProblemDetails CreateProblemDetails(ValidationException exception, HttpContext httpContext)
    {
        static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
                return input;

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        var problemDetails = new ValidationProblemDetails
        {
            Title = "Validation Error",
            Detail = exception.Message,
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = UnprocessableContentProblemType,
            Instance = httpContext.Request.Path.Value,
        };

        foreach (var error in exception.Errors)
        {
            problemDetails.Errors.Add(ToCamelCase(error.Key), [.. error.Value]);
        }

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }

    private async Task WriteProblemDetailsAsync(HttpContext httpContext, ValidationProblemDetails problem, CancellationToken token)
    {
        httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        httpContext.Response.ContentType = ApplicationProblemJsonMediaType;

        var service = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var result = await service.TryWriteAsync(new()
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        if (!result)
        {
            _logger.LogError("Failed to write ValidationProblemDetails to the response.");
            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await httpContext.Response.WriteAsync("An error occurred relating to an invalid request payload.", token);
        }
    }
}
