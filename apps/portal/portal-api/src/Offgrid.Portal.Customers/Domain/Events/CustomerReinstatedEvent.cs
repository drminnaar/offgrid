using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Events
{
    public sealed record CustomerReinstatedEvent(
        Guid CustomerId,
        DateTimeOffset OccurredAt
    ) : IDomainEvent
    {
        public string EventId => EventRegistry.Customer.CustomerReinstatedEventId;

        public string EventName => nameof(CustomerReinstatedEvent);

        public string CorrelationId => CustomerId.ToString();
    }
}
