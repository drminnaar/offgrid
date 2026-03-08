using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetProductByIdActionHandler
{
    public const string EndpointName = "GetProductById";

    public static async Task<IResult> GetProductByIdAsync(
        [FromServices] IProductService productService,
        [FromRoute(Name = "productId")] string productId,
        CancellationToken token = default)
    {
        var result = await productService.GetProductByIdAsync(productId, token);

        return TypedResults.Ok(result);
    }
}
