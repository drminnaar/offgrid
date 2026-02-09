using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Events
{
    public sealed record CustomerReinstatedEvent(
        Guid CustomerId,
        DateTimeOffset OccurredAt,
        string Reason
    ) : IDomainEvent
    {
        public string EventTypeId => EventRegistry.Customer.CustomerReinstatedEventId;

        public string EventType => nameof(CustomerReinstatedEvent);

        public string CorrelationId => CustomerId.ToString();
    }
}
