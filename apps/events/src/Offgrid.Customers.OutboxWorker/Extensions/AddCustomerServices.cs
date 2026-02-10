using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Customers.OutboxWorker.Application.Services;
using Offgrid.Customers.OutboxWorker.Domain.Services;
using Offgrid.Customers.OutboxWorker.Infrastructure.Messaging;
using Offgrid.Customers.OutboxWorker.Infrastructure.Persistence.Repositories;

namespace Offgrid.Customers.OutboxWorker.Extensions;

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
