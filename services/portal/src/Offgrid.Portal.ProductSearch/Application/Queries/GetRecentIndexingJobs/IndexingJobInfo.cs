namespace Offgrid.Portal.ProductSearch.Application.Queries.GetRecentIndexingJobs;

/// <summary>
/// Represents information about an indexing job.
/// </summary>
public sealed record IndexingJobInfo
{
    /// <summary>
    /// Gets the unique identifier of the indexing job.
    /// </summary>
    public Guid JobId { get; init; }

    /// <summary>
    /// Gets the status of the indexing job.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Gets the date and time when the indexing job was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time when the indexing job was completed, if applicable.
    /// </summary>
    public DateTimeOffset? CompletedAt { get; init; }
}
