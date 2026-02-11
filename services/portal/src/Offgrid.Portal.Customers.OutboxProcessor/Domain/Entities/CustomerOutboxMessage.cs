namespace Offgrid.Portal.Customers.OutboxProcessor.Domain.Entities;

public sealed class CustomerOutboxMessage : OutboxMessageBase
{
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid Id { get; private set; }
    public string EventTypeId { get; private set; } = string.Empty;
    public string EventType { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public DateTimeOffset OccurredAt { get; private set; }
    public DateTimeOffset? ProcessedAt { get; private set; }
    public string? Error { get; private set; }
    public int RetryCount { get; private set; }
    public DateTimeOffset? NextRetryAt { get; private set; }
    public bool IsDeadletter { get; private set; }

    private CustomerOutboxMessage() : base()
    {
    }

    public override bool IsPending(DateTimeOffset nextRetryTime)
    {
        return !IsDeadletter
            && ProcessedAt == null
            && (NextRetryAt == null || NextRetryAt <= nextRetryTime);
    }

    public override void MarkAsProcessed(DateTimeOffset processedAt)
    {
        ProcessedAt = processedAt;
        Error = null;
        IsDeadletter = false;
    }

    public override void MarkAsFailed(string error, DateTimeOffset failedAt, TimeSpan retryDelay)
    {
        Error = error;
        RetryCount++;
        NextRetryAt = failedAt.Add(retryDelay);
        IsDeadletter = false;
    }

    public override void MarkAsDeadLettered(DateTimeOffset deadLetteredAt)
    {
        ProcessedAt = deadLetteredAt;
        IsDeadletter = true;
    }
}
