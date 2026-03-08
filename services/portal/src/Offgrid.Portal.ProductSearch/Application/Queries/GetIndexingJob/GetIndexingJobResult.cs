namespace Offgrid.Portal.ProductSearch.Application.Queries.GetIndexingJob;

/// <summary>
/// Represents the result of a query to retrieve information about an indexing job, including its
/// unique identifier, status, creation time, and completion time (if applicable).
/// </summary>
public sealed record GetIndexingJobResult
{
    /// <summary>
    /// Gets the unique identifier of the indexing job.
    /// </summary>
    /// <value>The unique identifier of the indexing job.</value>
    public required Guid JobId { get; init; }

    /// <summary>
    /// Gets the current status of the indexing job.
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
    public required string Status { get; init; }

    /// <summary>
    /// Gets the creation time of the indexing job.
    /// </summary>
    /// <value>The creation time of the indexing job.</value>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the completion time of the indexing job, if it has completed.
    /// </summary>
    /// <value>The completion time of the indexing job, or <c>null</c> if the job has not completed.</value>
    public required DateTimeOffset? CompletedAt { get; init; }
}
