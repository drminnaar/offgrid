using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Offgrid.ShopApi.ExceptionHandlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private const string ApplicationProblemJsonMediaType = "application/problem+json";
    private const string InternalServerErrorProblemType = "https://tools.ietf.org/html/rfc9110#section-15.6.1";

    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly TimeProvider _timeProvider;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment environment, TimeProvider timeProvider)
    {
        _logger = logger;
        _environment = environment;
        _timeProvider = timeProvider;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occurred at {OccurredAt}.", _timeProvider.GetUtcNow().ToString("O"));
        var problemDetails = CreateProblemDetails(httpContext, exception);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = ApplicationProblemJsonMediaType;
        await JsonSerializer.SerializeAsync(httpContext.Response.Body, problemDetails, cancellationToken: cancellationToken);
        return true;
    }

    private ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
    {
        ProblemDetails? problemDetails;
        if (_environment.IsProduction())
        {
            problemDetails = CreateGeneralProblemDetails(httpContext);
        }
        else
        {
            problemDetails = CreateNotForProdProblemDetails(httpContext, exception, _timeProvider);
        }
        problemDetails.Extensions.TryAdd("requestId", httpContext.Request?.HttpContext.TraceIdentifier ?? string.Empty);
        problemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? string.Empty);
        problemDetails.Extensions.TryAdd("timestamp", _timeProvider.GetUtcNow().ToString("O"));
        return problemDetails;
    }

    private static ProblemDetails CreateNotForProdProblemDetails(HttpContext context, Exception exception, TimeProvider timeProvider)
    {
        var problemHttpContext = ProblemHttpContext.FromHttpContext(context, Activity.Current?.Id);
        var problemException = ProblemException.FromException(exception, timeProvider);

        var problem = new ProblemDetails()
        {
            Detail = exception.Message,
            Instance = context.Request?.Path ?? string.Empty,
            Status = StatusCodes.Status500InternalServerError,
            Title = $"{exception.GetType().Name}",
            Type = InternalServerErrorProblemType
        };
        problem.Extensions.TryAdd("exception", problemException);
        problem.Extensions.TryAdd("httpContext", problemHttpContext);
        return problem;
    }

    private static ProblemDetails CreateGeneralProblemDetails(HttpContext context)
    {
        return new ProblemDetails()
        {
            Detail = "A server fault occurred while processing your request.",
            Instance = context.Request?.Path.Value ?? string.Empty,
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal server fault.",
            Type = InternalServerErrorProblemType
        };
    }

    private sealed record ProblemException
    {
        private ProblemException()
        {
        }

        public required string Message { get; init; }
        public required string StackTrace { get; init; }
        public required string Source { get; init; }
        public required string TimeStamp { get; init; }
        public required IEnumerable<ProblemException> InnerExceptions { get; init; } = [];

        internal static ProblemException FromException(Exception exception, TimeProvider timeProvider)
        {
            var timeStamp = timeProvider.GetUtcNow().ToString("O");

            return new ProblemException
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace ?? string.Empty,
                Source = exception.Source ?? string.Empty,
                TimeStamp = timeStamp,
                InnerExceptions = exception switch
                {
                    AggregateException aggEx => aggEx.InnerExceptions.Select(innerEx => FromException(innerEx, timeProvider)),
                    _ when exception.InnerException != null => new[] { FromException(exception.InnerException, timeProvider) },
                    _ => []
                }
            };
        }
    }

    private sealed record ProblemHttpContext
    {
        private static readonly HashSet<string> SensitiveHeaders = new(StringComparer.OrdinalIgnoreCase)
        {
            "Authorization", "Cookie", "X-API-Key", "X-Auth-Token"
        };

        private ProblemHttpContext()
        {
        }

        public required string RoutePattern { get; init; }
        public required string TraceId { get; init; }
        public required RequestInfo Request { get; init; }

        internal sealed record RequestInfo
        {
            public required Dictionary<string, string> Headers { get; init; } = [];
            public required string Host { get; init; }
            public required string Method { get; init; }
            public required string Path { get; init; }
            public required string Protocol { get; init; }
            public required Dictionary<string, string> QueryParams { get; init; } = [];
            public required string QueryString { get; init; }
            public required string RequestId { get; init; }
            public required string Scheme { get; init; }
            public required string Value { get; init; }
        }

        internal static ProblemHttpContext FromHttpContext(HttpContext httpContext, string? traceId = "")
        {
            var endpoint = httpContext.GetEndpoint();
            var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText ?? string.Empty;

            var headers = httpContext.Request.Headers
                .Where(h => !SensitiveHeaders.Contains(h.Key))
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            return new()
            {
                RoutePattern = routePattern,
                TraceId = traceId ?? string.Empty,
                Request = new RequestInfo
                {
                    Headers = headers,
                    Host = httpContext.Request.Host.Value ?? string.Empty,
                    Method = httpContext.Request.Method ?? string.Empty,
                    Path = httpContext.Request.Path.Value ?? string.Empty,
                    Protocol = httpContext.Request.Protocol ?? string.Empty,
                    QueryParams = httpContext.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()),
                    QueryString = httpContext.Request.QueryString.Value ?? string.Empty,
                    Scheme = httpContext.Request.Scheme ?? string.Empty,
                    RequestId = httpContext.TraceIdentifier ?? string.Empty,
                    Value = $"{httpContext.Request.Method} {httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString} {httpContext.Request.Protocol}"
                }
            };
        }
    }
}
