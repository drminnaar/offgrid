using Microsoft.Extensions.Logging;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Domain.Events;

namespace Offgrid.Portal.Customers.Application.EventHandlers;

public sealed class CustomerChangedEventHandler : IDomainEventHandler<CustomerChangedEvent>
{
    private readonly ILogger<CustomerChangedEventHandler> _logger;

    public CustomerChangedEventHandler(ILogger<CustomerChangedEventHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    public Task HandleAsync(CustomerChangedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Handled {EventName} for customer {CustomerId} at {OccurredAt}.",
            domainEvent.EventName,
            domainEvent.CustomerId,
            domainEvent.OccurredAt);

        // TODO: Add additional logic for handling customer changes:
        // - Update search index
        // - Publish integration event to message bus
        // - Notify other services

        return Task.CompletedTask;
    }
}
