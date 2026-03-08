namespace Offgrid.Portal.ProductSearch.Application.Queries.GetCurrentIndexingJob;

/// <summary>
/// Represents the result of a query to retrieve information about the current (if any) indexing job.
/// </summary>
public sealed record GetCurrentIndexingJobResult
{
    /// <summary>
    /// Gets the unique identifier of the current indexing job.
    /// </summary>
    /// <value>The unique identifier of the current indexing job.</value>
    public Guid? JobId { get; init; }

    /// <summary>
    /// Gets the status of the current indexing job.
    /// </summary>
    /// <remarks>
    /// The status can be one of the following values: "Pending", "InProgress", "Completed",
    /// "FailedAndRetrying", or "Deadlettered". When the job is pending, it means that it is waiting to
    /// be processed. When the job is in progress, it means that it is currently being indexed.
    /// When the job is completed, it means that the indexing process has finished successfully.
    /// When the job has failed, it means that an error occurred during the indexing process,
    /// and that it is in the state of retrying.
    /// When the job is deadlettered, it means that the job could not be indexed successfully
    /// and has been moved to a dead letter queue.
    /// </remarks>
    /// <value>The current status of the indexing job.</value>
    public string? Status { get; init; }
}
