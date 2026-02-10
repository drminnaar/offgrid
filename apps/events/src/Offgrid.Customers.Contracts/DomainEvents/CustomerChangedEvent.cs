using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Events;

namespace Offgrid.Customers.Contracts.DomainEvents;

public sealed record CustomerChangedEvent : IDomainEvent, IHasValueChanges
{
    public CustomerChangedEvent(
        Guid customerId,
        DateTimeOffset occurredAt,
        string changedBy,
        IReadOnlyCollection<Change> changes)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(changedBy, nameof(changedBy));
        ArgumentNullException.ThrowIfNull(changes, nameof(changes));
        CustomerId = customerId;
        AggregateId = customerId.ToString();
        OccurredAt = occurredAt;
        ChangedBy = changedBy;
        Changes = changes;
    }

    public Guid CustomerId { get; }

    public DateTimeOffset OccurredAt { get; }

    public string EventTypeId => EventRegistry.Customer.CustomerChangedEventTypeId;

    public string EventType => nameof(CustomerChangedEvent);

    public string CorrelationId => CustomerId.ToString();

    public string ChangedBy { get; }

    public IReadOnlyCollection<Change> Changes { get; }

    public string AggregateId { get; }
}
