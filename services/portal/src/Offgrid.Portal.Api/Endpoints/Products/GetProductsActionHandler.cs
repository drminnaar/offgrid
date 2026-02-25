using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Products.Application.Queries.GetProducts;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetProductsActionHandler
{
    public const string EndpointName = "GetProducts";

    public static async Task<IResult> GetProductsAsync(
        [FromServices] IProductService productService,
        HttpContext httpContext,
        [FromQuery(Name = "brands")] string brands = "",
        [FromQuery(Name = "categories")] string categories = "",
        [FromQuery(Name = "types")] string types = "",
        [FromQuery(Name = "page")] int pageNumber = GetProductsQuery.DefaultPageNumber,
        [FromQuery(Name = "limit")] int pageSize = GetProductsQuery.DefaultPageSize,
        CancellationToken token = default)
    {
        var _ = httpContext
            .Username()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var query = new GetProductsQuery
        {
            Page = pageNumber,
            PageSize = pageSize,
            Brands = brands ?? string.Empty,
            Categories = categories ?? string.Empty,
            Types = types ?? string.Empty
        };

        var result = await productService.GetProductsAsync(query, token);

        return TypedResults.Ok(result);
    }
}
