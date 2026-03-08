using Microsoft.Extensions.DependencyInjection.Extensions;
using Offgrid.Framework.Domain;
using Offgrid.Framework.EntityFrameworkCore.Extensions;
using Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetCurrentIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetRecentIndexingJobs;
using Offgrid.Portal.ProductSearch.Domain.Services;
using Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework;
using Offgrid.Portal.ProductSearch.Infrastructure.Persistence.EntityFramework.Repositories;

namespace Offgrid.Portal.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddProductSearchServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        // add infrastructure services
        services.TryAddSingleton(TimeProvider.System);
        services.TryAddSingleton<IEntityIdGenerator, EntityIdGenerator>();
        services.AddOffgridDbContext<IJobDbContext, JobDbContext>(
            configuration,
            enableDetailedErrors: !environment.IsProduction(),
            enableSensitiveDataLogging: !environment.IsProduction());
        services.AddScoped<IIndexingJobRepository, IndexingJobRepository>();

        // add application services
        services.AddScoped<ICreateIndexingJobHandler, CreateIndexingJobHandler>();
        services.AddScoped<IGetIndexingJobQueryHandler, GetIndexingJobQueryHandler>();
        services.AddScoped<IGetCurrentIndexingJobQueryHandler, GetCurrentIndexingJobQueryHandler>();
        services.AddScoped<IGetRecentIndexingJobsQueryHandler, GetRecentIndexingJobsQueryHandler>();

        return services;
    }
}
