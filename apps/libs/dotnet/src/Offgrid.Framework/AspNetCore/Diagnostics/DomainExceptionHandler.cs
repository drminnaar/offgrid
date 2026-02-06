using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Offgrid.Framework.Exceptions;

namespace Offgrid.Framework.AspNetCore.Diagnostics;

public sealed class DomainExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DomainExceptionHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public DomainExceptionHandler(ILogger<DomainExceptionHandler> logger, TimeProvider timeProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DomainException domainException)
            return false;

        var problemDetails = CreateProblemDetails(domainException, httpContext);

        await WriteProblemDetailsAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    private ValidationProblemDetails CreateProblemDetails(DomainException exception, HttpContext httpContext)
    {
        static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
                return input;

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        var problemDetails = new ValidationProblemDetails
        {
            Title = "Business Rule Violation",
            Detail = exception.Message,
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = ProblemTypeNames.Status422UnprocessableContent,
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
            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await httpContext.Response.WriteAsync("An error occurred relating to an invalid request payload.", token);
        }
    }
}
