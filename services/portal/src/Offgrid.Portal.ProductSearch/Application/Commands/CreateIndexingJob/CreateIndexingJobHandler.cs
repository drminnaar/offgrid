using Offgrid.Framework.Domain;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;

public interface ICreateIndexingJobHandler
{
    Task<CreateIndexingJobResult> HandleAsync(CancellationToken cancellationToken = default);
}

public sealed class CreateIndexingJobHandler : ICreateIndexingJobHandler
{
    private readonly IIndexingJobRepository _jobRepository;
    private readonly IEntityIdGenerator _entityIdGenerator;
    private readonly TimeProvider _timeProvider;

    public CreateIndexingJobHandler(
        IIndexingJobRepository jobRepository,
        IEntityIdGenerator entityIdGenerator,
        TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(jobRepository, nameof(jobRepository));
        ArgumentNullException.ThrowIfNull(entityIdGenerator, nameof(entityIdGenerator));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _jobRepository = jobRepository;
        _entityIdGenerator = entityIdGenerator;
        _timeProvider = timeProvider;
    }

    public async Task<CreateIndexingJobResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var time = _timeProvider.GetUtcNow();
        var jobId = _entityIdGenerator.GenerateEntityId();
        var job = IndexingJob.CreateNew(jobId, time);
        _jobRepository.AddJob(job);
        await _jobRepository.SaveChangesAsync(cancellationToken);
        return new CreateIndexingJobResult(jobId);
    }
}
