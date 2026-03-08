using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Offgrid.Portal.ProductSearch.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductSearchInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.TryAddSingleton(TimeProvider.System);

        return services
            .AddEntityFrameworkInfrastructure(configuration, environment)
            .AddMongoDbInfrastructure(configuration)
            .AddTypeSenseInfrastructure(configuration)
            .AddProductSearchApplication();
    }
}
