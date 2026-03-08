using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Application.Queries.GetRecentIndexingJobs;

/// <summary>
/// Defines the contract for handling queries to retrieve summary of recent indexing jobs.
/// </summary>
public interface IGetRecentIndexingJobsQueryHandler
{
    /// <summary>
    /// Handles the query to retrieve information about recent indexing jobs.
    /// </summary>
    /// <param name="count">The number of recent indexing jobs to retrieve.</param>
    /// <param name="token">A cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a
    /// list of <see cref="IndexingJobInfo"/> objects.
    /// </returns>
    Task<IReadOnlyList<IndexingJobInfo>> HandleAsync(int count, CancellationToken token = default);
}

public sealed class GetRecentIndexingJobsQueryHandler : IGetRecentIndexingJobsQueryHandler
{
    private readonly IIndexingJobRepository _repository;

    public GetRecentIndexingJobsQueryHandler(IIndexingJobRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        _repository = repository;
    }

    public async Task<IReadOnlyList<IndexingJobInfo>> HandleAsync(int count, CancellationToken token = default)
    {
        var jobs = await _repository.GetRecentJobsAsync(count, token);
        return jobs.Select(job => new IndexingJobInfo
        {
            JobId = job.Id,
            Status = job.DetermineStatus().ToString(),
            CreatedAt = job.CreatedAt,
            CompletedAt = job.CompletedAt
        }).ToList();
    }
}
