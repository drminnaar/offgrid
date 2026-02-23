namespace Offgrid.Portal.Api.Endpoints.ProductCategories;

public static class ProductCategoriesEndpointMap
{
    internal const string PREFIX = "/product-categories";

    public static WebApplication MapProductCategoriesEndpoints(this WebApplication app)
    {
        app
            .MapGroup(PREFIX)
            .MapGetAllProductCategories()
            .RequireAuthorization();
        return app;
    }

    private static RouteGroupBuilder MapGetAllProductCategories(this RouteGroupBuilder route)
    {
        route
            .MapGet("", GetProductCategoriesActionHandler.GetProductCategoriesAsync)
            .WithName(GetProductCategoriesActionHandler.EndpointName);
        return route;
    }
}
