using Microsoft.Extensions.Options;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;
using RabbitMQ.Client;

namespace Offgrid.Portal.Customers.EventProcessor.Application.Consumers;

public sealed class CustomerReinstatedEventConsumer : RabbitMqCloudEventConsumer<CustomerReinstatedEvent>
{
    public CustomerReinstatedEventConsumer(
        ILogger<RabbitMqCloudEventConsumer<CustomerReinstatedEvent>> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> settings,
        IEventHandler<CustomerReinstatedEvent> eventHandler) : base(logger, connectionFactory, settings, eventHandler)
    {
    }

    protected override string QueueName => QueueNameRegistry.CustomerReinstatedEventQueue;

    protected override string RoutingKey => "com.offgrid.portal.customers.customer-reinstated";
}
