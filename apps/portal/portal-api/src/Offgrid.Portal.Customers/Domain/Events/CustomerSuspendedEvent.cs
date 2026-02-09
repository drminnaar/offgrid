using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Events
{
    public sealed record CustomerSuspendedEvent(
        Guid CustomerId,
        DateTimeOffset OccurredAt,
        string Reason
    ) : IDomainEvent
    {
        public string EventTypeId => EventRegistry.Customer.CustomerSuspendedEventId;

        public string EventType => nameof(CustomerSuspendedEvent);

        public string CorrelationId => CustomerId.ToString();
    }
}
