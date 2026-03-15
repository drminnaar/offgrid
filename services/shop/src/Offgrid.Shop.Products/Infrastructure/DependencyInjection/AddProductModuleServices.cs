using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Offgrid.Shop.Products.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductModuleServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        return services
            .AddTypeSenseInfrastructure(configuration)
            .AddApplicationServices();
    }
}
