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
                self = links.GetUriByName(context, "Root"),
                customers = new
                {
                    href = CustomerEndpointMap.PREFIX,
                    getAll = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{CustomerEndpointMap.PREFIX}",
                        method = "GET"
                    },
                    suspend = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{CustomerEndpointMap.PREFIX}/{{customerId}}/suspend",
                        templated = true,
                        method = "POST"
                    },
                    reinstate = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{CustomerEndpointMap.PREFIX}/{{customerId}}/reinstate",
                        templated = true,
                        method = "POST"
                    }
                }
            };

            return Results.Ok(new
            {
                name = "Offgrid Portal API",
                version = "2.0.0",
                description = "API for Offgrid Portal application",
                _links = baseLinks
            });
        }).WithName("Root");
    }
}
