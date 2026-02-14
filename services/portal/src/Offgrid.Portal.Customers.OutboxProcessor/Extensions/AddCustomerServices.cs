using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Portal.Customers.OutboxProcessor.Application.Services;
using Offgrid.Portal.Customers.OutboxProcessor.Domain.Services;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence.Repositories;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;
using Offgrid.Framework.Messaging;
using CloudNative.CloudEvents;
using Offgrid.Framework.RabbitMq;
using Offgrid.Framework.CncfCloudEvents;
using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.OutboxProcessor.Extensions;

public static class CustomerServiceExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        // add framework services
        services.TryAddSingleton(TimeProvider.System);

        // add infrastructure services
        services.AddSingleton<ICloudEventIdProvider, CloudEventIdProvider>();
        services.AddSingleton<CloudEventFactoryBase<IDomainEvent>, CloudEventFactory>();
        services.AddSingleton<RabbitMqCloudEventPublisher>();
        services.AddSingleton<IEventPublisher<CloudEvent>, MessageBusPublisher>();

        // add application services
        services.AddScoped<ICustomerOutboxService, CustomerOutboxService>();

        // add domain services
        services.AddScoped<IOutbox, CustomerOutboxRepository>();

        return services;
    }
}
