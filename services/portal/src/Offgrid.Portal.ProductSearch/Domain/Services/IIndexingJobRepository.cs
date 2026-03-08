using Offgrid.Portal.ProductSearch.Domain.Entities;

namespace Offgrid.Portal.ProductSearch.Domain.Services;

/// <summary>
/// Defines the contract for a repository that manages indexing jobs.
/// </summary>
public interface IIndexingJobRepository
{
    /// <summary>
    /// Gets an indexing job by its unique identifier.
    /// </summary>
    /// <param name="jobId">The unique identifier of the indexing job.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The <see cref="IndexingJob"/> with the specified ID, or <c>null</c> if not found.</returns>
    Task<IndexingJob?> GetIndexingJobAsync(Guid jobId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current indexing job (in progress) from the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The current <see cref="IndexingJob"/>, or <c>null</c> if no job is in progress.</returns>
    Task<IndexingJob?> GetCurrentJobAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the next pending indexing job from the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The next pending <see cref="IndexingJob"/>, or <c>null</c> if no jobs are available.</returns>
    Task<IndexingJob?> GetNextJobAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new indexing job to the repository.
    /// </summary>
    /// <param name="job">The indexing job to add.</param>
    void AddJob(IndexingJob job);

    /// <summary>
    /// Saves changes to the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
