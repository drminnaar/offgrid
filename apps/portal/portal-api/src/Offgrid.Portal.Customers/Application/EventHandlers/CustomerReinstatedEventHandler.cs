using Microsoft.Extensions.Logging;
using Offgrid.Customers.Contracts.DomainEvents;
using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Application.EventHandlers;

public sealed class CustomerReinstatedEventHandler : IDomainEventHandler<CustomerReinstatedEvent>
{
    private readonly ILogger<CustomerReinstatedEventHandler> _logger;

    public CustomerReinstatedEventHandler(ILogger<CustomerReinstatedEventHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    public Task HandleAsync(CustomerReinstatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Handled {EventName} for customer {CustomerId} at {OccurredAt}.",
            domainEvent.EventType,
            domainEvent.CustomerId,
            domainEvent.OccurredAt);

        // TODO: Add additional logic for reinstating a customer:
        // - Send welcome back email
        // - Publish integration event to message bus
        // - Update analytics
        // - Notify other services

        return Task.CompletedTask;
    }
}
