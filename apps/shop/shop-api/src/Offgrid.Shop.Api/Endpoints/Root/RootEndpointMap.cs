using Offgrid.Shop.Api.Endpoints.Customers;

namespace Offgrid.Shop.Api.Endpoints.Root;

public static class RootEndpointMap
{
    public static void MapRootEndpoint(this WebApplication app)
    {
        app.MapGet("/", (HttpContext context, LinkGenerator links) =>
        {
            var baseLinks = new
            {
                customers = links.GetUriByName(context, UpsertCustomerHandler.EndpointName)
            };

            return Results.Ok(new
            {
                name = "Offgrid Shop API",
                version = "1.0.0",
                description = "API for Offgrid Shop application",
                _links = baseLinks
            });
        });
    }
}
