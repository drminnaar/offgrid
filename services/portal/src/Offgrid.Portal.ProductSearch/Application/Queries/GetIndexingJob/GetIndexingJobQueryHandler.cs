using Offgrid.Framework.Exceptions;
using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Application.Queries.GetIndexingJob;

/// <summary>
/// Defines the contract for handling queries to retrieve information about a specific indexing job.
/// </summary>
public interface IGetIndexingJobQueryHandler
{
    /// <summary>
    /// Handles the query to retrieve information about a specific indexing job by its unique identifier.
    /// </summary>
    /// <param name="jobId">The unique identifier of the indexing job.</param>
    /// <param name="token">A cancellation token.</param>
    /// <returns>A <see cref="GetIndexingJobResult"/> containing information about the indexing job.</returns>
    /// <exception cref="EntityNotFoundException">Thrown if the indexing job with the specified ID is not found.</exception>
    Task<GetIndexingJobResult> HandleAsync(Guid jobId, CancellationToken token = default);
}

public sealed class GetIndexingJobQueryHandler : IGetIndexingJobQueryHandler
{
    private readonly IIndexingJobRepository _repository;

    public GetIndexingJobQueryHandler(IIndexingJobRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        _repository = repository;
    }

    public async Task<GetIndexingJobResult> HandleAsync(Guid jobId, CancellationToken token = default)
    {
        var job = await _repository
            .GetIndexingJobAsync(jobId, token)
            ?? throw new EntityNotFoundException($"Indexing job with ID '{jobId}' not found.")
            {
                EntityKey = jobId.ToString(),
                EntityType = nameof(Domain.Entities.IndexingJob)
            };

        return new GetIndexingJobResult
        {
            JobId = job.Id,
            Status = job.DetermineStatus().ToString(),
            CreatedAt = job.CreatedAt,
            CompletedAt = job.CompletedAt
        };
    }
}
