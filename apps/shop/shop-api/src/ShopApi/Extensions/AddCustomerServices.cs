using Offgrid.Customers.Application.Commands.UpsertCustomer;
using Offgrid.Customers.Application.Services;
using Offgrid.Customers.Domain.Repositories;
using Offgrid.Customers.Domain.Services;
using Offgrid.Customers.Infrastructure.Persistence.Repositories;

namespace Offgrid.ShopApi.Extensions;

public static partial class CustomerServiceExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerIdGenerator, CustomerIdGenerator>();
        services.AddSingleton<ICustomerNumberGenerator, CustomerNumberGenerator>();
        services.AddSingleton<ICustomerFactory, CustomerFactory>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUpsertCustomerCommandHandler, UpsertCustomerCommandHandler>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
