namespace Offgrid.Portal.ProductSearch.Domain.Entities;

/// <summary>
/// Represents a job for indexing products in the product search system.
/// </summary>
public sealed class IndexingJob
{
    public Guid Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public bool IsIndexing { get; private set; }
    public string? Error { get; private set; }
    public int RetryCount { get; private set; } = 0;
    public DateTimeOffset? NextRetryAt { get; private set; }
    public bool IsDeadletter { get; private set; }

    private IndexingJob() : base()
    {
    }

    /// <summary>
    /// Creates a new instance of the IndexingJob class with the specified ID and creation time.
    /// </summary>
    /// <param name="id">The unique identifier for the indexing job.</param>
    /// <param name="createdAt">The creation time of the indexing job.</param>
    /// <returns>A new instance of the <see cref="IndexingJob"/> class.</returns>
    public static IndexingJob CreateNew(Guid id, DateTimeOffset createdAt)
    {
        return new IndexingJob
        {
            Id = id,
            CreatedAt = createdAt,
            RetryCount = 0,
            IsDeadletter = false,
            IsIndexing = false
        };
    }

    /// <summary>
    /// Determines whether the job is pending and ready to be processed.
    /// A job is considered pending if it is not deadlettered, has not been processed,
    /// and its next retry time is either null or less than or equal to the specified time.
    /// </summary>
    /// <param name="nextRetryTime">The time to compare against the job's next retry time.</param>
    /// <returns>True if the job is pending; otherwise, false.</returns>
    public bool IsPending(DateTimeOffset nextRetryTime)
    {
        return !IsDeadletter
            && CompletedAt == null
            && !IsIndexing
            && (NextRetryAt == null || NextRetryAt <= nextRetryTime);
    }

    /// <summary>
    /// Marks the job as completed by setting the CompletedAt timestamp to the current time.
    /// It also clears any existing error and resets the IsDeadletter flag to false.
    /// </summary>
    /// <param name="completedAt">The time when the job was completed.</param>
    public void MarkAsCompleted(DateTimeOffset completedAt)
    {
        CompletedAt = completedAt;
        Error = null;
        IsDeadletter = false;
        IsIndexing = false;
    }

    /// <summary>
    /// Marks the job as failed by setting the error message, incrementing the retry count,
    /// and calculating the next retry time based on the provided failed time and retry delay.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <param name="failedAt">The time when the job failed.</param>
    /// <param name="retryDelay">The delay before the next retry attempt.</param>
    public void MarkAsFailed(string error, DateTimeOffset failedAt, TimeSpan retryDelay)
    {
        Error = error;
        RetryCount++;
        NextRetryAt = failedAt.Add(retryDelay);
        IsDeadletter = false;
        IsIndexing = false;
    }

    /// <summary>
    /// Marks the job as dead lettered by setting the CompletedAt timestamp to the specified
    /// time and setting the IsDeadletter flag to true. This indicates that the job has 
    /// failed permanently and should not be retried.
    /// </summary>
    /// <param name="deadLetteredAt">The time when the job was marked as dead lettered.</param>
    public void MarkAsDeadLettered(DateTimeOffset deadLetteredAt)
    {
        CompletedAt = deadLetteredAt;
        IsDeadletter = true;
        IsIndexing = false;
    }

    /// <summary>
    /// Marks the job as currently being indexed by setting the IsIndexing flag to true.
    /// </summary>
    public void MarkAsIndexing()
    {
        IsIndexing = true;
    }

    /// <summary>
    /// Determines the current status of the indexing job.
    /// </summary>
    /// <returns>The <see cref="IndexingJobStatus"/> representing the status of the job.</returns>
    public IndexingJobStatus DetermineStatus()
    {
        if (IsDeadletter)
        {
            return IndexingJobStatus.Deadlettered;
        }
        else if (IsIndexing)
        {
            return IndexingJobStatus.InProgress;
        }
        else if (CompletedAt != null && Error == null)
        {
            return IndexingJobStatus.Completed;
        }
        else if (Error != null)
        {
            return IndexingJobStatus.FailedAndRetrying;
        }
        else
        {
            return IndexingJobStatus.Pending;
        }
    }
}
