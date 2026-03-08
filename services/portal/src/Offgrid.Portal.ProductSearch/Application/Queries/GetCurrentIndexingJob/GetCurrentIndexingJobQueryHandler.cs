using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Application.Queries.GetCurrentIndexingJob;

/// <summary>
/// Defines the contract for handling queries to retrieve information about the current indexing job.
/// </summary>
public interface IGetCurrentIndexingJobQueryHandler
{
    /// <summary>
    /// Handles the query to retrieve information about the current indexing job.
    /// </summary>
    /// <param name="token">A cancellation token.</param>
    /// <returns>A <see cref="GetCurrentIndexingJobResult"/> containing information about the current indexing job.</returns>
    Task<GetCurrentIndexingJobResult> HandleAsync(CancellationToken token = default);
}

public sealed class GetCurrentIndexingJobQueryHandler : IGetCurrentIndexingJobQueryHandler
{
    private readonly IIndexingJobRepository _repository;

    public GetCurrentIndexingJobQueryHandler(IIndexingJobRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        _repository = repository;
    }

    public async Task<GetCurrentIndexingJobResult> HandleAsync(CancellationToken token = default)
    {
        var job = await _repository.GetCurrentJobAsync(token);

        return new GetCurrentIndexingJobResult
        {
            JobId = job?.Id,
            Status = job?.DetermineStatus().ToString() ?? string.Empty,
        };
    }
}
