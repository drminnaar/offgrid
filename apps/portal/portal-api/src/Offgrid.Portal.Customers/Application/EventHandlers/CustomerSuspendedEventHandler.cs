using Microsoft.Extensions.Logging;
using Offgrid.Customers.Contracts.DomainEvents;
using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Application.EventHandlers;

public sealed class CustomerSuspendedEventHandler : IDomainEventHandler<CustomerSuspendedEvent>
{
    private readonly ILogger<CustomerSuspendedEventHandler> _logger;

    public CustomerSuspendedEventHandler(ILogger<CustomerSuspendedEventHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    public Task HandleAsync(CustomerSuspendedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Handled {EventName} for customer {CustomerId} at {OccurredAt}.",
            domainEvent.EventType,
            domainEvent.CustomerId,
            domainEvent.OccurredAt);

        // TODO: Add additional logic for suspending a customer:
        // - Send deactivation confirmation email
        // - Remove customer from keycloak groups
        // - Publish integration event to message bus
        // - Revoke access tokens

        return Task.CompletedTask;
    }
}
