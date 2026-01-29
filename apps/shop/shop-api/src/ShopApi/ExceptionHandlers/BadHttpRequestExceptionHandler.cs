using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Offgrid.ShopApi.ExceptionHandlers;

public sealed class BadHttpRequestExceptionHandler : IExceptionHandler
{
    private const string ApplicationProblemJsonMediaType = "application/problem+json";
    private const string BadRequestProblemType = "https://tools.ietf.org/html/rfc9110#section-15.5.1";

    private readonly ILogger<BadHttpRequestExceptionHandler> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly TimeProvider _timeProvider;

    public BadHttpRequestExceptionHandler(ILogger<BadHttpRequestExceptionHandler> logger, IWebHostEnvironment environment, TimeProvider timeProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException badHttpException)
            return false;

        _logger.LogWarning(badHttpException, "Invalid request payload for resource at {Path}", httpContext.Request.Path);

        var problemDetails = CreateProblemDetails(badHttpException, httpContext);

        await WriteProblemDetailsAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    private ValidationProblemDetails CreateProblemDetails(BadHttpRequestException exception, HttpContext httpContext)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Invalid request payload",
            Type = BadRequestProblemType,
            Detail = _environment.IsDevelopment()
                ? exception.Message
                : $"Invalid (or missing) request payload for resource at {httpContext.Request.Path}",
            Instance = httpContext.Request.Path.Value,
        };

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }

    private async Task WriteProblemDetailsAsync(HttpContext httpContext, ProblemDetails problem, CancellationToken ct)
    {
        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status400BadRequest;
        httpContext.Response.ContentType = ApplicationProblemJsonMediaType;

        var service = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var result = await service.TryWriteAsync(new()
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        if (!result)
        {
            _logger.LogError("Failed to write ProblemDetails response for BadHttpRequestException.");
            await httpContext.Response.WriteAsync("An error occurred relating to an invalid request payload.", ct);
        }
    }
}
