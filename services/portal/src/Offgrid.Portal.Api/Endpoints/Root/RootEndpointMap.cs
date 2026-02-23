using Offgrid.Portal.Api.Endpoints.Customers;
using Offgrid.Portal.Api.Endpoints.ProductBrands;
using Offgrid.Portal.Api.Endpoints.ProductCategories;
using Offgrid.Portal.Api.Endpoints.Products;
using Offgrid.Portal.Api.Endpoints.ProductTypes;

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
                },
                products = new
                {
                    href = ProductsEndpointMap.PREFIX,
                    getAll = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{ProductsEndpointMap.PREFIX}",
                        method = "GET"
                    }
                },
                productBrands = new
                {
                    href = ProductBrandsEndpointMap.PREFIX,
                    getAll = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{ProductBrandsEndpointMap.PREFIX}",
                        method = "GET"
                    }
                },
                productCategories = new
                {
                    href = ProductCategoriesEndpointMap.PREFIX,
                    getAll = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{ProductCategoriesEndpointMap.PREFIX}",
                        method = "GET"
                    }
                },
                productTypes = new
                {
                    href = ProductTypesEndpointMap.PREFIX,
                    getAll = new
                    {
                        href = $"{context.Request.Scheme}://{context.Request.Host}{ProductTypesEndpointMap.PREFIX}",
                        method = "GET"
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
