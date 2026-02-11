using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Contracts.DomainEvents;

public sealed record CustomerSuspendedEvent : IDomainEvent
{
    public CustomerSuspendedEvent(Guid customerId, DateTimeOffset occurredAt, string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason, nameof(reason));
        CustomerId = customerId;
        OccurredAt = occurredAt;
        Reason = reason;
        AggregateId = customerId.ToString();
        EventTypeId = EventRegistry.Customer.CustomerSuspendedEventTypeId;
        EventType = nameof(CustomerSuspendedEvent);
        CorrelationId = customerId.ToString();
    }

    public string EventTypeId { get; }

    public string EventType { get; }

    public string CorrelationId { get; }

    public string AggregateId { get; }

    public DateTimeOffset OccurredAt { get; }

    public Guid CustomerId { get; }

    public string Reason { get; }
}
