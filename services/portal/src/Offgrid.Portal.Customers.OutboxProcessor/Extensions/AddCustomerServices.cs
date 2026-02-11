using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Portal.Customers.OutboxProcessor.Application.Services;
using Offgrid.Portal.Customers.OutboxProcessor.Domain.Services;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence.Repositories;
using Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Messaging;

namespace Offgrid.Portal.Customers.OutboxProcessor.Extensions;

public static class CustomerServiceExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        // add framework services
        services.TryAddSingleton(TimeProvider.System);

        // add domain services
        services.AddScoped<IOutbox, CustomerOutboxRepository>();

        // add application services
        services.AddScoped<ICloudEventIdProvider, CloudEventIdProvider>();
        services.AddScoped<ICloudEventFactory, CloudEventFactory>();
        services.AddScoped<ICustomerOutboxService, CustomerOutboxService>();
        services.AddScoped<IEventPublisher, LoggingPublisher>();

        return services;
    }
}
