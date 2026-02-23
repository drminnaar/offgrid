namespace Offgrid.Portal.Api.Endpoints.Products;

public static class ProductsEndpointMap
{
    internal const string PREFIX = "/products";

    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app
            .MapGroup(PREFIX)
            .MapGetAllProducts()
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
}
