using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Offgrid.Framework.AspNetCore.Diagnostics;

public sealed class UnauthorizedAccessExceptionHandler : IExceptionHandler
{
    private readonly ILogger<UnauthorizedAccessExceptionHandler> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly TimeProvider _timeProvider;

    public UnauthorizedAccessExceptionHandler(
        ILogger<UnauthorizedAccessExceptionHandler> logger,
        IWebHostEnvironment environment,
        TimeProvider timeProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedAccessException unauthorizedAccessException)
            return false;

        _logger.LogWarning(unauthorizedAccessException, "Unauthorized access attempt at {Path}", httpContext.Request.Path);

        var problemDetails = CreateProblemDetails(unauthorizedAccessException, httpContext);

        await WriteProblemDetailsAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(UnauthorizedAccessException exception, HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = ProblemTypeNames.Status401Unauthorized,
            Detail = _environment.IsDevelopment()
                ? exception.Message
                : $"Access to resource ({httpContext.Request.Path}) is unauthorized.",
            Instance = httpContext.Request.Path.Value,
        };

        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }

    private async Task WriteProblemDetailsAsync(HttpContext httpContext, ProblemDetails problem, CancellationToken ct)
    {
        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status401Unauthorized;
        httpContext.Response.ContentType = ContentTypeNames.Application.ProblemDetailsJson;

        var service = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var result = await service.TryWriteAsync(new()
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        if (!result)
        {
            _logger.LogError("Failed to write ProblemDetails response for UnauthorizedAccessException.");
            await httpContext.Response.WriteAsync("An error occurred relating to an unauthorized request.", ct);
        }
    }
}
