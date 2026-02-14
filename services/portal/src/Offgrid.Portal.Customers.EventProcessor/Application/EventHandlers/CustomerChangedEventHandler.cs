using Offgrid.Framework.Messaging;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.EventProcessor.Application.EventHandlers;

public sealed class CustomerChangedEventHandler : IEventHandler<CustomerChangedEvent>
{
    private readonly ILogger<CustomerChangedEventHandler> _logger;

    public CustomerChangedEventHandler(ILogger<CustomerChangedEventHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    public Task<bool> HandleAsync(CustomerChangedEvent @event, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event, nameof(@event));
        _logger.LogInformation(
            "Handled {EventName} for customer {CustomerId} at {OccurredAt}.",
            @event.EventType,
            @event.CustomerId,
            @event.OccurredAt);

        // TODO: Add additional logic for handling customer changes:
        // - Update search index
        // - Notify other services

        return Task.FromResult(true);
    }
}
