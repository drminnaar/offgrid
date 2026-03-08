using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.ProductSearch.Domain.Services;
using Offgrid.Portal.ProductSearch.Infrastructure.Persistence.MongoDb.Repositories;

namespace Offgrid.Portal.ProductSearch.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    private static IServiceCollection AddMongoDbInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services
            .AddOptions<MongoDatabaseOptions>()
            .Bind(configuration.GetRequiredSection(MongoDatabaseOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.ConnectionString), "Mongo connection string is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.DatabaseName), "Mongo database name is required.")
            .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDatabaseOptions>>().Value);

        services.AddSingleton<IMongoCollectionProvider, MongoCollectionProvider>();
        services.AddSingleton<IProductCatalog, ProductRepository>();

        return services;
    }
}
