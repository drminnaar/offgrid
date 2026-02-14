using Microsoft.Extensions.Options;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;
using RabbitMQ.Client;

namespace Offgrid.Portal.Customers.EventProcessor.Application.Consumers;

public sealed class CustomerSuspendedEventConsumer : RabbitMqCloudEventConsumer<CustomerSuspendedEvent>
{
    public CustomerSuspendedEventConsumer(
        ILogger<RabbitMqCloudEventConsumer<CustomerSuspendedEvent>> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> settings,
        IEventHandler<CustomerSuspendedEvent> eventHandler) : base(logger, connectionFactory, settings, eventHandler)
    {
    }

    protected override string QueueName => QueueNameRegistry.CustomerSuspendedEventQueue;

    protected override string RoutingKey => "com.offgrid.portal.customers.customer-suspended";
}
