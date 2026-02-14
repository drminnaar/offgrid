using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;

namespace Offgrid.Portal.Customers.EventProcessor;

public sealed class CustomerReinstatedEventWorker : BackgroundService
{
    private readonly RabbitMqCloudEventConsumer<CustomerReinstatedEvent> _consumer;

    public CustomerReinstatedEventWorker(RabbitMqCloudEventConsumer<CustomerReinstatedEvent> consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer, nameof(consumer));
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.ConsumeAsync(stoppingToken);
    }
}
