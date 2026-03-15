using Microsoft.Extensions.DependencyInjection;
using Offgrid.Shop.Products.Application.Queries;
using Offgrid.Shop.Products.Application.Services;

namespace Offgrid.Shop.Products.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        return services
            .AddScoped<ISearchProductsQueryHandler, SearchProductsQueryHandler>();
    }
}
