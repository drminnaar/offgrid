using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Portal.ProductSearch.Indexor.Infrastructure.Configuration;

namespace Offgrid.Portal.ProductSearch.Indexor.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductSearchIndexor(
        this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.TryAddSingleton(TimeProvider.System);

        services
            .AddOptions<IndexingOptions>()
            .Bind(configuration.GetRequiredSection(IndexingOptions.SectionName))
            .Validate(options => options.PollingIntervalSeconds > 0, "PollingIntervalInSeconds must be greater than 0.")
            .ValidateOnStart();

        services.AddHostedService<ProductIndexWorker>();

        return services;
    }
}
