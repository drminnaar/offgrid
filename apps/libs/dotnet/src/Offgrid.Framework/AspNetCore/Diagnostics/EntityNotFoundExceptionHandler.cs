using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Offgrid.Framework.Exceptions;

namespace Offgrid.Framework.AspNetCore.Diagnostics;

public sealed class EntityNotFoundExceptionHandler : IExceptionHandler
{
    private readonly ILogger<EntityNotFoundExceptionHandler> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly TimeProvider _timeProvider;

    public EntityNotFoundExceptionHandler(ILogger<EntityNotFoundExceptionHandler> logger, IWebHostEnvironment environment, TimeProvider timeProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not EntityNotFoundException entityNotFoundException)
            return false;

        if (_environment.IsDevelopment())
        {
            _logger.LogWarning(
                "{MissingResource} with ID {ResourceId} not found at {Path}",
                entityNotFoundException.EntityType,
                entityNotFoundException.EntityKey,
                httpContext.Request.Path);
        }

        var problemDetails = CreateProblemDetails(entityNotFoundException, httpContext);

        await WriteProblemDetailsAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    private ValidationProblemDetails CreateProblemDetails(EntityNotFoundException exception, HttpContext httpContext)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Resource not found",
            Type = ProblemTypeNames.Status404NotFound,
            Detail = _environment.IsDevelopment()
                ? exception.Message
                : $"{exception.EntityType} with ID {exception.EntityKey} not found.",
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
        httpContext.Response.ContentType = ContentTypeNames.Application.ProblemDetailsJson;

        var service = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var result = await service.TryWriteAsync(new()
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        if (!result)
        {
            _logger.LogError($"Failed to write ProblemDetails response for {nameof(EntityNotFoundException)}.");
            await httpContext.Response.WriteAsync("An error occurred relating to a resource not found.", ct);
        }
    }
}

