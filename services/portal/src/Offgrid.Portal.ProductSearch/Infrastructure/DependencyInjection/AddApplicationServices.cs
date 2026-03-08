using Microsoft.Extensions.DependencyInjection;
using Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Commands.ProcessIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetCurrentIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetRecentIndexingJobs;

namespace Offgrid.Portal.ProductSearch.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductSearchApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IProcessIndexingJobHandler, ProcessIndexingJobHandler>();
        services.AddScoped<ICreateIndexingJobHandler, CreateIndexingJobHandler>();
        services.AddScoped<IGetIndexingJobQueryHandler, GetIndexingJobQueryHandler>();
        services.AddScoped<IGetCurrentIndexingJobQueryHandler, GetCurrentIndexingJobQueryHandler>();
        services.AddScoped<IGetRecentIndexingJobsQueryHandler, GetRecentIndexingJobsQueryHandler>();

        return services;
    }
}
