using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Events
{
    public sealed record CustomerChangedEvent : IDomainEvent, IHasValueChanges
    {
        public CustomerChangedEvent(
            Guid customerId,
            DateTimeOffset occurredAt,
            ValueChanges customerChanges)
        {
            ArgumentNullException.ThrowIfNull(customerChanges, nameof(customerChanges));
            CustomerId = customerId;
            OccurredAt = occurredAt;
            _customerChanges = customerChanges;
        }

        public Guid CustomerId { get; }
        public DateTimeOffset OccurredAt { get; }

        public string EventTypeId => EventRegistry.Customer.CustomerChangedEventId;

        public string EventType => nameof(CustomerChangedEvent);

        public string CorrelationId => CustomerId.ToString();

        public string ChangedBy => _customerChanges.ChangedBy;

        private readonly ValueChanges _customerChanges;
        public IReadOnlyCollection<Change> Changes => _customerChanges.Changes;
    }
}
