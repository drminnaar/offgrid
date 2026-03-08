using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetProductVariantsActionHandler
{
    public const string EndpointName = "GetProductVariants";

    public static async Task<IResult> GetProductVariantsAsync(
        [FromServices] IProductService productService,
        [FromRoute(Name = "productId")] string productId,
        CancellationToken token = default)
    {
        var result = await productService.GetProductVariantsAsync(productId, token);

        return TypedResults.Ok(result);
    }
}
