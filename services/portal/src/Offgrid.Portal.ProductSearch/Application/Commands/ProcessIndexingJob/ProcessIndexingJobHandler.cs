using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Domain.Mappers;
using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Application.Commands.ProcessIndexingJob;

public interface IProcessIndexingJobHandler
{
    Task<ProcessIndexingJobResult> HandleAsync(CancellationToken cancellationToken = default);
}

public sealed class ProcessIndexingJobHandler : IProcessIndexingJobHandler
{
    private readonly ILogger<ProcessIndexingJobHandler> _logger;
    private readonly IIndexingJobRepository _jobRepository;
    private readonly IProductCatalog _productCatalog;
    private readonly IProductSearchIndexer _indexer;
    private readonly TimeProvider _timeProvider;

    public ProcessIndexingJobHandler(
        ILogger<ProcessIndexingJobHandler> logger,
        IIndexingJobRepository jobRepository,
        IProductCatalog productCatalog,
        IProductSearchIndexer indexer,
        TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(jobRepository, nameof(jobRepository));
        ArgumentNullException.ThrowIfNull(productCatalog, nameof(productCatalog));
        ArgumentNullException.ThrowIfNull(indexer, nameof(indexer));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _logger = logger;
        _jobRepository = jobRepository;
        _productCatalog = productCatalog;
        _indexer = indexer;
        _timeProvider = timeProvider;
    }

    public async Task<ProcessIndexingJobResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        var job = await _jobRepository.GetNextJobAsync(cancellationToken);
        if (job == null)
        {
            _logger.LogInformation("No pending indexing job found.");
            return ProcessIndexingJobResult.NoJobsFound();
        }

        _logger.LogInformation("Processing indexing job {JobId}", job.Id);

        job.MarkAsIndexing();
        await _jobRepository.SaveChangesAsync(cancellationToken);
        await IndexProductsAsync(job, cancellationToken);
        await _jobRepository.SaveChangesAsync(cancellationToken);

        stopwatch.Stop();
        _logger.LogInformation("Completed processing indexing job {JobId}. Duration: {DurationMs}ms ({DurationSeconds}s)",
            job.Id, stopwatch.ElapsedMilliseconds, stopwatch.Elapsed.TotalSeconds);

        return ProcessIndexingJobResult.Create(
            job.Id,
            job.DetermineStatus(),
            stopwatch.ElapsedMilliseconds,
            stopwatch.Elapsed.TotalSeconds);
    }

    private async Task IndexProductsAsync(IndexingJob job, CancellationToken cancellationToken)
    {
        if (job.IsPending(_timeProvider.GetUtcNow()))
        {
            return;
        }

        try
        {
            var products = await _productCatalog.GetAvailableProductsAsync(cancellationToken);

            var searchDocuments = products
                .Select(product => product.ToProductSearchDocuments())
                .SelectMany(docs => docs)
                .ToList();

            await _indexer.DeleteAndRecreateCollectionAsync(cancellationToken);

            await _indexer.IndexCollectionAsync(searchDocuments, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing indexing job {JobId}. Marking as failed.", job.Id);
            MarkAsFailed(job, ex);
            return; // Exit the method to avoid marking the job as completed
        }

        job.MarkAsCompleted(_timeProvider.GetUtcNow());
    }

    private void MarkAsFailed(IndexingJob job, Exception ex, int maxRetries = 5)
    {
        job.MarkAsFailed(ex.Message, _timeProvider.GetUtcNow(), CalculateExponentialBackoffDelay(job.RetryCount));

        if (job.RetryCount >= maxRetries)
        {
            _logger.LogError("Indexing job {JobId} failed permanently after {Retries} attempts", job.Id, maxRetries);
            job.MarkAsDeadLettered(_timeProvider.GetUtcNow());
        }
        else
        {
            _logger.LogWarning("Indexing job {JobId} failed with error: {Error}. It will be retried at {NextRetryAt}. Retry count: {RetryCount}", job.Id, ex.Message, job.NextRetryAt, job.RetryCount);
        }
    }

    private static TimeSpan CalculateExponentialBackoffDelay(int retryCount)
    {
        // Exponential backoff: 30s, 1min, 2min, 10min, etc.
        return TimeSpan.FromSeconds(Math.Pow(2, retryCount) * 30);
    }
}
