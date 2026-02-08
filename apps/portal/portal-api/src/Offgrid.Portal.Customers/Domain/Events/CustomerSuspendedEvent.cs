using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Events
{
    public sealed record CustomerSuspendedEvent(
        Guid CustomerId,
        DateTimeOffset OccurredAt
    ) : IDomainEvent
    {
        public string EventId => EventRegistry.Customer.CustomerSuspendedEventId;

        public string EventName => nameof(CustomerSuspendedEvent);

        public string CorrelationId => CustomerId.ToString();
    }
}
