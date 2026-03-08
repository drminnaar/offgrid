using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Offgrid.Framework.Domain;
using Offgrid.Framework.EntityFrameworkCore.Extensions;
using Offgrid.Portal.ProductSearch.Domain.Services;
using Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework;
using Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework.Repositories;

namespace Offgrid.Portal.ProductSearch.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    private static IServiceCollection AddEntityFrameworkInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentNullException.ThrowIfNull(environment, nameof(environment));

        services.TryAddSingleton<IEntityIdGenerator, EntityIdGenerator>();

        services.AddOffgridDbContext<IJobDbContext, JobDbContext>(
            configuration,
            enableDetailedErrors: !environment.IsProduction(),
            enableSensitiveDataLogging: !environment.IsProduction());

        services.AddScoped<IIndexingJobRepository, IndexingJobRepository>();

        return services;
    }
}
