using Microsoft.Extensions.Options;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Application.Queries.GetProductBrands;
using Offgrid.Portal.Products.Application.Queries.GetProductCategories;
using Offgrid.Portal.Products.Application.Queries.GetProducts;
using Offgrid.Portal.Products.Application.Queries.GetProductTypes;
using Offgrid.Portal.Products.Application.Services;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddProductServices(this IServiceCollection services, IConfiguration configuration)
    {
        // add infrastructure services
        services.Configure<MongoDatabaseOptions>(configuration.GetSection("MongoDatabaseOptions"));
        services.AddSingleton(resolver =>
        {
            return resolver.GetRequiredService<IOptions<MongoDatabaseOptions>>().Value;
        });
        services.AddSingleton<IMongoCollectionProvider, MongoCollectionProvider>();
        services.AddSingleton<IMongoRepository<Product>, MongoRepository<Product>>(sp =>
        {
            var provider = sp.GetRequiredService<IMongoCollectionProvider>();
            var logger = sp.GetRequiredService<ILogger<MongoRepository<Product>>>();
            return new MongoRepository<Product>(logger, provider, collectionName: IProductService.CollectionName);
        });

        // add application service query handlers
        services.AddScoped<IGetProductsHandler, GetProductsHandler>();
        services.AddScoped<IGetProductTypesHandler, GetProductTypesHandler>();
        services.AddScoped<IGetProductCategoriesHandler, GetProductCategoriesHandler>();
        services.AddScoped<IGetProductBrandsHandler, GetProductBrandsHandler>();

        // add application services
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
