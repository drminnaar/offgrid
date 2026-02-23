namespace Offgrid.Portal.Api.Endpoints.ProductTypes;

public static class ProductTypesEndpointMap
{
    internal const string PREFIX = "/product-types";

    public static WebApplication MapProductTypesEndpoints(this WebApplication app)
    {
        app
            .MapGroup(PREFIX)
            .MapGetAllProductTypes()
            .RequireAuthorization();
        return app;
    }

    private static RouteGroupBuilder MapGetAllProductTypes(this RouteGroupBuilder route)
    {
        route
            .MapGet("", GetProductTypesActionHandler.GetProductTypesAsync)
            .WithName(GetProductTypesActionHandler.EndpointName);
        return route;
    }
}
