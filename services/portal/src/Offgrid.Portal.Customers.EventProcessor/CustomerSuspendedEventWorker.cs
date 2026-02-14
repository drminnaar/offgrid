using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.EventProcessor;

public sealed class CustomerSuspendedEventWorker : BackgroundService
{
    private readonly RabbitMqCloudEventConsumer<CustomerSuspendedEvent> _consumer;

    public CustomerSuspendedEventWorker(RabbitMqCloudEventConsumer<CustomerSuspendedEvent> consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer, nameof(consumer));
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.ConsumeAsync(stoppingToken);
    }
}
