namespace Offgrid.Portal.Customers.OutboxProcessor.Domain.Entities;

public abstract class OutboxMessageBase
{
    protected OutboxMessageBase()
    {
    }

    /// <summary>
    /// Determines whether the message is pending and ready to be processed.
    /// A message is considered pending if it is not deadlettered, has not been processed,
    /// and its next retry time is either null or less than or equal to the specified time.
    /// </summary>
    /// <param name="nextRetryTime">The time to compare against the message's next retry time.</param>
    /// <returns>True if the message is pending; otherwise, false.</returns>
    public abstract bool IsPending(DateTimeOffset nextRetryTime);

    /// <summary>
    /// Marks the message as processed by setting the ProcessedAt timestamp to the current time.
    /// It also clears any existing error and resets the IsDeadletter flag to false.
    /// </summary>
    /// <param name="processedAt">The time when the message was processed.</param>
    public abstract void MarkAsProcessed(DateTimeOffset processedAt);

    /// <summary>
    /// Marks the message as failed by setting the error message, incrementing the retry count,
    /// and calculating the next retry time based on the provided failed time and retry delay.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <param name="failedAt">The time when the message failed.</param>
    /// <param name="retryDelay">The delay before the next retry attempt.</param>
    public abstract void MarkAsFailed(string error, DateTimeOffset failedAt, TimeSpan retryDelay);

    /// <summary>
    /// Marks the message as dead lettered by setting the ProcessedAt timestamp to the specified
    /// time and setting the IsDeadletter flag to true. This indicates that the message has 
    /// failed permanently and should not be retried.
    /// </summary>
    /// <param name="deadLetteredAt">The time when the message was marked as dead lettered.</param>
    public abstract void MarkAsDeadLettered(DateTimeOffset deadLetteredAt);
}
