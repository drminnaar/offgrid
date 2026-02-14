using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.EventProcessor;

public sealed class CustomerChangedEventWorker : BackgroundService
{
    private readonly RabbitMqCloudEventConsumer<CustomerChangedEvent> _consumer;

    public CustomerChangedEventWorker(RabbitMqCloudEventConsumer<CustomerChangedEvent> consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer, nameof(consumer));
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.ConsumeAsync(stoppingToken);
    }
}
