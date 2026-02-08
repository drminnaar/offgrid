using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;
using Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;
using Offgrid.Portal.Customers.Application.EventHandlers;
using Offgrid.Portal.Customers.Application.Services;
using Offgrid.Portal.Customers.Domain.Events;
using Offgrid.Portal.Customers.Domain.Repositories;
using Offgrid.Portal.Customers.Domain.Services;
using Offgrid.Portal.Customers.Infrastructure.Events;
using Offgrid.Portal.Customers.Infrastructure.Persistence;
using Offgrid.Portal.Customers.Infrastructure.Persistence.Repositories;

namespace Offgrid.Portal.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        // add general framework services
        services.TryAddSingleton(TimeProvider.System);

        // add offgrid framework services
        services.TryAddScoped<IEntityIdGenerator, EntityIdGenerator>();

        // add infrastructure services
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerChangeRepository, CustomerChangeRepository>();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();
        services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // add domain services
        services.AddScoped<ICustomerChangeFactory, CustomerChangeFactory>();

        // add domain event handlers
        services.AddScoped<IDomainEventHandler<CustomerChangedEvent>, CustomerChangedEventHandler>();
        services.AddScoped<IDomainEventHandler<CustomerSuspendedEvent>, CustomerSuspendedEventHandler>();
        services.AddScoped<IDomainEventHandler<CustomerReinstatedEvent>, CustomerReinstatedEventHandler>();

        // add application service command handlers
        services.AddScoped<IReinstateCustomerCommandHandler, ReinstateCustomerCommandHandler>();
        services.AddScoped<ISuspendCustomerCommandHandler, SuspendCustomerCommandHandler>();

        // add application services
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
