using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework.Repositories;

public sealed class IndexingJobRepository : IIndexingJobRepository
{
    private readonly IJobDbContext _dbContext;
    private readonly TimeProvider _timeProvider;

    public IndexingJobRepository(IJobDbContext dbContext, TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _dbContext = dbContext;
        _timeProvider = timeProvider;
    }

    public async Task<IndexingJob?> GetNextJobAsync(CancellationToken cancellationToken = default)
    {
        var now = _timeProvider.GetUtcNow();
        return await _dbContext
            .IndexingJobs
            .TagWith("Get next pending indexing job")
            .Where(message => !message.IsDeadletter
                && message.CompletedAt == null
                && (message.NextRetryAt == null || message.NextRetryAt <= now))
            .OrderBy(job => job.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void AddJob(IndexingJob job)
    {
        ArgumentNullException.ThrowIfNull(job, nameof(job));
        _dbContext.IndexingJobs.Add(job);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!_dbContext.HasChanges())
        {
            return;
        }

        var stateEntriesWritten = await _dbContext.SaveChangesAsync(cancellationToken);

        if (stateEntriesWritten <= 0)
        {
            throw new InvalidOperationException("Failed to save changes to the database. No state entries were written.");
        }
    }

    public Task<IndexingJob?> GetIndexingJobAsync(Guid jobId, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .IndexingJobs
            .AsNoTracking()
            .TagWith($"Get indexing job by ID: {jobId}")
            .FirstOrDefaultAsync(job => job.Id == jobId, cancellationToken);
    }

    public Task<IndexingJob?> GetCurrentJobAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext
            .IndexingJobs
            .AsNoTracking()
            .TagWith("Get current indexing job")
            .Where(job => !job.IsDeadletter && job.CompletedAt == null)
            .OrderBy(job => job.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
