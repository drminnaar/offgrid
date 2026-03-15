namespace Offgrid.Shop.Api.Endpoints.Products;

public static class ProductsEndpointMap
{
    private const string PREFIX = "/products";

    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(PREFIX);
        group.MapSearchProducts();
        return app;
    }

    private static RouteGroupBuilder MapSearchProducts(this RouteGroupBuilder route)
    {
        route
            .MapGet("", SearchProductsHandler.SearchProductsAsync)
            .WithName(SearchProductsHandler.EndpointName);
        return route;
    }
}
