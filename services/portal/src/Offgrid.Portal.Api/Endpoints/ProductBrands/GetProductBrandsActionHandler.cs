using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.ProductBrands;

public static class GetProductBrandsActionHandler
{
    public const string EndpointName = "GetProductBrands";

    public static async Task<IResult> GetProductBrandsAsync(
        [FromServices] IProductService productService,
        CancellationToken token = default)
    {
        var result = await productService.GetProductBrandsAsync(token);

        return TypedResults.Ok(result);
    }
}
