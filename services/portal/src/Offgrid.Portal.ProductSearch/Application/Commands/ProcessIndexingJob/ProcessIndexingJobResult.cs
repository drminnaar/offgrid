using Offgrid.Portal.ProductSearch.Domain.Entities;

namespace Offgrid.Portal.ProductSearch.Application.Commands.ProcessIndexingJob;

/// <summary>
/// Represents the result of processing an indexing job, including details about the job and its outcome.
/// </summary>
public sealed record ProcessIndexingJobResult
{
    /// <summary>
    /// Creates a <see cref="ProcessIndexingJobResult"/> representing a scenario where no jobs were found.
    /// </summary>
    /// <returns>A <see cref="ProcessIndexingJobResult"/> indicating no jobs were found.</returns>
    public static ProcessIndexingJobResult NoJobsFound()
    {
        return new ProcessIndexingJobResult
        {
            Outcome = "NoJobsFound"
        };
    }

    /// <summary>
    /// Creates a <see cref="ProcessIndexingJobResult"/> representing the outcome of processing an
    /// indexing job, including the job ID, status, and processing times.
    /// </summary>
    /// <param name="jobId">The ID of the indexing job.</param>
    /// <param name="status">The status of the indexing job.</param>
    /// <param name="durationMilliseconds">The time taken to process in milliseconds.</param>
    /// <param name="durationSeconds">The time taken to process in seconds.</param>
    /// <returns>A <see cref="ProcessIndexingJobResult"/> representing the outcome of the indexing job.</returns>
    public static ProcessIndexingJobResult Create(
        Guid jobId,
        IndexingJobStatus status,
        long durationMilliseconds,
        double durationSeconds)
    {
        return new ProcessIndexingJobResult
        {
            JobId = jobId,
            Outcome = status.ToString(),
            DurationMilliseconds = durationMilliseconds,
            DurationSeconds = durationSeconds
        };
    }

    public Guid? JobId { get; private set; }
    public long? DurationMilliseconds { get; private set; }
    public double? DurationSeconds { get; private set; }
    public required string Outcome { get; init; }
}
