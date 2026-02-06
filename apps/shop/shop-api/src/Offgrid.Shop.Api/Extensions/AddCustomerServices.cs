using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Shop.Customers.Application.Commands.UpsertCustomer;
using Offgrid.Shop.Customers.Application.Services;
using Offgrid.Shop.Customers.Domain.Repositories;
using Offgrid.Shop.Customers.Domain.Services;
using Offgrid.Shop.Customers.Infrastructure.Persistence.Repositories;

namespace Offgrid.Shop.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        // add general framework services
        services.TryAddSingleton(TimeProvider.System);

        // add infrastructure services
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // add application services
        services.AddScoped<IUpsertCustomerCommandHandler, UpsertCustomerCommandHandler>();
        services.AddScoped<ICustomerService, CustomerService>();

        // add domain services
        services.AddSingleton<ICustomerIdGenerator, CustomerIdGenerator>();
        services.AddSingleton<ICustomerNumberGenerator, CustomerNumberGenerator>();
        services.AddSingleton<ICustomerFactory, CustomerFactory>();

        return services;
    }
}
