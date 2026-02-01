using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Offgrid.ShopApi.Middleware;

public sealed class UnauthorizedProblemDetailsMiddleware
{
    private const string ApplicationProblemJsonContentType = "application/problem+json";
    private const string UnauthorizedProblemType = "https://tools.ietf.org/html/rfc9110#section-15.5.2";

    private readonly RequestDelegate _next;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<UnauthorizedProblemDetailsMiddleware> _logger;

    public UnauthorizedProblemDetailsMiddleware(RequestDelegate next, TimeProvider timeProvider, ILogger<UnauthorizedProblemDetailsMiddleware> logger)
    {
        _next = next;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized && !context.Response.HasStarted)
        {
            var problemDetails = CreateProblemDetails(context);
            await WriteProblemDetailsAsync(context, problemDetails, CancellationToken.None);
        }
    }

    private ProblemDetails CreateProblemDetails(HttpContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = UnauthorizedProblemType,
            Title = "Unauthorized",
            Detail = $"Access to resource ({context.Request.Path}) is unauthorized.",
            Instance = context.Request.Path
        };

        problemDetails.Extensions.TryAdd("requestId", context.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));

        return problemDetails;
    }

    private async Task WriteProblemDetailsAsync(HttpContext httpContext, ProblemDetails problem, CancellationToken ct)
    {
        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status401Unauthorized;
        httpContext.Response.ContentType = ApplicationProblemJsonContentType;

        var service = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var result = await service.TryWriteAsync(new()
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        if (!result)
        {
            _logger.LogError($"Failed to write ProblemDetails response for {nameof(UnauthorizedProblemDetailsMiddleware)}.");
            await httpContext.Response.WriteAsync("An error occurred relating to an unauthorised request.", ct);
        }
    }
}
