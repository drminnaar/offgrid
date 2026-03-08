using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.ProductTypes;

public static class GetProductTypesActionHandler
{
    public const string EndpointName = "GetProductTypes";

    public static async Task<IResult> GetProductTypesAsync(
        [FromServices] IProductService productService,
        CancellationToken token = default)
    {
        var result = await productService.GetProductTypesAsync(token);

        return TypedResults.Ok(result);
    }
}
