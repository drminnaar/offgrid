using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Offgrid.Framework.Exceptions;
using Offgrid.Framework.System;

namespace Offgrid.Framework.AspNetCore.Diagnostics;

public sealed class ValidationExceptionHandler : IExceptionHandler
{
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
        var problemDetails = new ValidationProblemDetails
        {
            Title = "Validation Error",
            Detail = exception.Message,
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = ProblemTypeNames.Status422UnprocessableContent,
            Instance = httpContext.Request.Path.Value,
        };

        foreach (var error in exception.Errors)
        {
            problemDetails.Errors.Add(error.Key.ToCamelCase(), [.. error.Value]);
        }

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }

    private async Task WriteProblemDetailsAsync(HttpContext httpContext, ValidationProblemDetails problem, CancellationToken token)
    {
        httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        httpContext.Response.ContentType = ContentTypeNames.Application.ProblemDetailsJson;

        var service = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var result = await service.TryWriteAsync(new()
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        if (!result)
        {
            _logger.LogError("Failed to write ValidationProblemDetails to the response.");
            await httpContext.Response.WriteAsync("An error occurred relating to an invalid request payload.", token);
        }
    }
}
