using Microsoft.Extensions.Options;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;
using RabbitMQ.Client;

namespace Offgrid.Portal.Customers.EventProcessor.Application.Consumers;

public sealed class CustomerChangedEventConsumer : RabbitMqCloudEventConsumer<CustomerChangedEvent>
{
    public CustomerChangedEventConsumer(
        ILogger<RabbitMqCloudEventConsumer<CustomerChangedEvent>> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> settings,
        IEventHandler<CustomerChangedEvent> messageHandler) : base(logger, connectionFactory, settings, messageHandler)
    {
    }

    protected override string QueueName => QueueNameRegistry.CustomerChangedEventQueue;

    protected override string RoutingKey => "com.offgrid.portal.customers.customer-changed";
}
