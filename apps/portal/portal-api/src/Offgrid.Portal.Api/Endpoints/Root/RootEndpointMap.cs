using Offgrid.Portal.Api.Endpoints.Customers;

namespace Offgrid.Portal.Api.Endpoints.Root;

public static class RootEndpointMap
{
    public static void MapRootEndpoint(this WebApplication app)
    {
        app.MapGet("/", (HttpContext context, LinkGenerator links) =>
        {
            var baseLinks = new
            {
                customers = links.GetUriByName(context, ChangeCustomerStatusHandler.EndpointName)
            };

            return Results.Ok(new
            {
                name = "Offgrid Portal API",
                version = "1.0.0",
                description = "API for Offgrid Portal application",
                _links = baseLinks
            });
        });
    }
}
