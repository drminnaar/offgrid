using Microsoft.Extensions.Options;
using Offgrid.Portal.ProductSearch.Application.Commands.ProcessIndexingJob;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Indexor.Infrastructure.Configuration;

namespace Offgrid.Portal.ProductSearch.Indexor;

public class ProductIndexWorker : BackgroundService
{
    private readonly ILogger<ProductIndexWorker> _logger;
    private readonly IndexingOptions _indexingOptions;
    private readonly TimeProvider _timeProvider;
    private readonly IServiceScopeFactory _scopeFactory;

    public ProductIndexWorker(
        ILogger<ProductIndexWorker> logger,
        IOptions<IndexingOptions> indexingOptions,
        TimeProvider timeProvider,
        IServiceScopeFactory scopeFactory)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(indexingOptions, nameof(indexingOptions));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        ArgumentNullException.ThrowIfNull(scopeFactory, nameof(scopeFactory));
        _logger = logger;
        _indexingOptions = indexingOptions.Value;
        _timeProvider = timeProvider;
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("{Worker} running at: {time}", nameof(ProductIndexWorker), _timeProvider.GetUtcNow());
            }
            try
            {
                await ProcessNextIndexingJobAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in worker loop");
            }
            await Task.Delay(_indexingOptions.PollingInterval, stoppingToken);
        }
    }

    private async Task ProcessNextIndexingJobAsync(CancellationToken cancellationToken)
    {
        // Each job gets its own DI scope — DbContext, MongoDB sessions, etc.
        // are properly fresh and disposed after each iteration.
        await using var scope = _scopeFactory.CreateAsyncScope();
        var command = scope.ServiceProvider.GetRequiredService<IProcessIndexingJobHandler>();

        var result = await command.HandleAsync(cancellationToken);
        if (result.JobId != null)
        {
            if (Enum.TryParse<IndexingJobStatus>(result.Outcome, out var status))
            {
                if (status == IndexingJobStatus.Completed)
                {
                    _logger.LogInformation("Indexing job {JobId} processed successfully", result.JobId);
                }
                else if (status == IndexingJobStatus.FailedAndRetrying)
                {
                    _logger.LogWarning("Indexing job {JobId} failed but will be retried. Duration: {DurationMs}ms ({DurationSeconds}s)",
                        result.JobId, result.DurationMilliseconds, result.DurationSeconds);
                }
                else if (status == IndexingJobStatus.Deadlettered)
                {
                    _logger.LogError("Indexing job {JobId} has been deadlettered after multiple failures. Duration: {DurationMs}ms ({DurationSeconds}s)",
                        result.JobId, result.DurationMilliseconds, result.DurationSeconds);
                }
            }
        }
        else
        {
            _logger.LogInformation("No indexing jobs found to process");
        }
    }
}
