namespace Offgrid.Portal.Customers.Domain.Entities;

public sealed class CustomerOutbox
{
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid Id { get; private set; }
    public string EventId { get; private set; } = string.Empty;
    public string EventType { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public DateTimeOffset OccurredAt { get; private set; }
    public DateTimeOffset? ProcessedAt { get; private set; }
    public string? Error { get; private set; }
    public int RetryCount { get; private set; }
    public DateTimeOffset? NextRetryAt { get; private set; }
    public bool IsDeadletter { get; private set; }

    private CustomerOutbox()
    {
    }

    public static CustomerOutbox CreateNew(
        Guid id,
        string eventId,
        string eventType,
        string payload,
        DateTimeOffset occurredAt,
        DateTimeOffset createdAt)
    {
        return new CustomerOutbox
        {
            Id = id,
            EventId = eventId,
            EventType = eventType,
            Payload = payload,
            OccurredAt = occurredAt,
            CreatedAt = createdAt,
            RetryCount = 0,
            IsDeadletter = false
        };
    }
}
