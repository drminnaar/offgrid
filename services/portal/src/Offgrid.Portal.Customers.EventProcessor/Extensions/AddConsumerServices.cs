using Offgrid.Framework.Messaging;
using Offgrid.Framework.RabbitMq;
using Offgrid.Portal.Customers.Contracts.DomainEvents;
using Offgrid.Portal.Customers.EventProcessor.Application.Consumers;
using Offgrid.Portal.Customers.EventProcessor.Application.EventHandlers;

namespace Offgrid.Portal.Customers.EventProcessor.Extensions;

public static partial class Extensions
{
    public static void AddConsumerServices(this IServiceCollection services)
    {
        // Register event handlers
        services.AddSingleton<IEventHandler<CustomerSuspendedEvent>, ConsoleCustomerSuspendedEventHandler>();
        services.AddSingleton<IEventHandler<CustomerChangedEvent>, ConsoleCustomerChangedEventHandler>();
        services.AddSingleton<IEventHandler<CustomerReinstatedEvent>, ConsoleCustomerReinstatedEventHandler>();

        // Register consumers
        services.AddSingleton<RabbitMqCloudEventConsumer<CustomerChangedEvent>, CustomerChangedEventConsumer>();
        services.AddSingleton<RabbitMqCloudEventConsumer<CustomerSuspendedEvent>, CustomerSuspendedEventConsumer>();
        services.AddSingleton<RabbitMqCloudEventConsumer<CustomerReinstatedEvent>, CustomerReinstatedEventConsumer>();
    }
}
