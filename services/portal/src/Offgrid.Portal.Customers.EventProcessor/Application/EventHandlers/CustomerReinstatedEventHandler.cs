using Offgrid.Framework.Messaging;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.EventProcessor.Application.EventHandlers;

public sealed class CustomerReinstatedEventHandler : IEventHandler<CustomerReinstatedEvent>
{
    private readonly ILogger<CustomerReinstatedEventHandler> _logger;

    public CustomerReinstatedEventHandler(ILogger<CustomerReinstatedEventHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    public Task<bool> HandleAsync(CustomerReinstatedEvent @event, CancellationToken cancellationToken = default)
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
