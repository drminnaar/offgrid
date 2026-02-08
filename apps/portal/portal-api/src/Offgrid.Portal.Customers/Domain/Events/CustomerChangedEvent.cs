using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Events
{
    public sealed record CustomerChangedEvent(
        Guid CustomerId,
        DateTimeOffset OccurredAt,
        ValueChanges Changes
    ) : IDomainEvent, IHasValueChanges
    {
        public string EventId => EventRegistry.Customer.CustomerChangedEventId;

        public string EventName => nameof(CustomerChangedEvent);

        public string CorrelationId => CustomerId.ToString();

        public string ChangedBy => Changes.ChangedBy;

        IReadOnlyCollection<Change> IHasValueChanges.Changes => Changes.Changes;
    }
}
