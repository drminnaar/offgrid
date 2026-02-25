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
}
