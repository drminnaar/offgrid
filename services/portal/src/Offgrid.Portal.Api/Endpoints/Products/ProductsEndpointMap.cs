namespace Offgrid.Portal.Api.Endpoints.Products;

public static class ProductsEndpointMap
{
    internal const string PREFIX = "/products";

    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app
            .MapGroup(PREFIX)
            .MapGetAllProducts()
            .MapGetProductById()
            .MapGetProductVariants()
            .MapGetProductIndex()
            .MapGetCurrentProductIndex()
            .MapCreateProductIndex()
            .MapGetRecentProductIndexingJobs()
            .RequireAuthorization();
        return app;
    }

    private static RouteGroupBuilder MapGetAllProducts(this RouteGroupBuilder route)
    {
        route
            .MapGet("", GetProductsActionHandler.GetProductsAsync)
            .WithName(GetProductsActionHandler.EndpointName);
        return route;
    }

    private static RouteGroupBuilder MapGetProductById(this RouteGroupBuilder route)
    {
        route
            .MapGet("/{productId}", GetProductByIdActionHandler.GetProductByIdAsync)
            .WithName(GetProductByIdActionHandler.EndpointName);
        return route;
    }

    private static RouteGroupBuilder MapGetProductVariants(this RouteGroupBuilder route)
    {
        route
            .MapGet("/{productId}/variants", GetProductVariantsActionHandler.GetProductVariantsAsync)
            .WithName(GetProductVariantsActionHandler.EndpointName);
        return route;
    }

    private static RouteGroupBuilder MapCreateProductIndex(this RouteGroupBuilder route)
    {
        route
            .MapPost("/indexes", CreateProductIndexActionHandler.CreateProductIndexAsync)
            .WithName(CreateProductIndexActionHandler.EndpointName);
        return route;
    }

    private static RouteGroupBuilder MapGetProductIndex(this RouteGroupBuilder route)
    {
        route
            .MapGet("/indexes/{jobId}", GetProductIndexByJobIdActionHandler.GetProductIndexByJobIdAsync)
            .WithName(GetProductIndexByJobIdActionHandler.EndpointName);
        return route;
    }

    private static RouteGroupBuilder MapGetCurrentProductIndex(this RouteGroupBuilder route)
    {
        route
            .MapGet("/indexes/current", GetCurrentProductIndexJobActionHandler.GetCurrentProductIndexJobAsync)
            .WithName(GetCurrentProductIndexJobActionHandler.EndpointName);
        return route;
    }

    private static RouteGroupBuilder MapGetRecentProductIndexingJobs(this RouteGroupBuilder route)
    {
        route
            .MapGet("/indexes/recent", GetRecentProductIndexingJobsActionHandler.GetRecentProductIndexingJobsAsync)
            .WithName(GetRecentProductIndexingJobsActionHandler.EndpointName);
        return route;
    }
}
