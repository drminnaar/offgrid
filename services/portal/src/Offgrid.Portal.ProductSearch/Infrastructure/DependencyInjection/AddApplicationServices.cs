using Microsoft.Extensions.DependencyInjection;
using Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Commands.ProcessIndexingJob;
using Offgrid.Portal.ProductSearch.Application.Queries.GetIndexingJob;

namespace Offgrid.Portal.ProductSearch.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductSearchApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IProcessIndexingJobHandler, ProcessIndexingJobHandler>();
        services.AddScoped<ICreateIndexingJobHandler, CreateIndexingJobHandler>();
        services.AddScoped<IGetIndexingJobQueryHandler, GetIndexingJobQueryHandler>();

        return services;
    }
}
