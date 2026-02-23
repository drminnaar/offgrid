namespace Offgrid.Portal.Api.Endpoints.ProductBrands;

public static class ProductBrandsEndpointMap
{
    internal const string PREFIX = "/product-brands";

    public static WebApplication MapProductBrandsEndpoints(this WebApplication app)
    {
        app
            .MapGroup(PREFIX)
            .MapGetAllProductBrands()
            .RequireAuthorization();
        return app;
    }

    private static RouteGroupBuilder MapGetAllProductBrands(this RouteGroupBuilder route)
    {
        route
            .MapGet("", GetProductBrandsActionHandler.GetProductBrandsAsync)
            .WithName(GetProductBrandsActionHandler.EndpointName);
        return route;
    }
}
