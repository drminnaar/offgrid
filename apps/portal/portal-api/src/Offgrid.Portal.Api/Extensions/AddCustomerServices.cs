using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;
using Offgrid.Portal.Customers.Application.Services;
using Offgrid.Portal.Customers.Domain.Repositories;
using Offgrid.Portal.Customers.Infrastructure.Persistence.Repositories;

namespace Offgrid.Portal.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        // add general framework services
        services.TryAddSingleton(TimeProvider.System);

        // add infrastructure services
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // add application services
        services.AddScoped<IChangeCustomerStatusCommandHandler, ChangeCustomerStatusCommandHandler>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
