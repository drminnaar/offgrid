using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetProductVariantsActionHandler
{
    public const string EndpointName = "GetProductVariants";

    public static async Task<IResult> GetProductVariantsAsync(
        [FromServices] IProductService productService,
        HttpContext httpContext,
        [FromRoute(Name = "productId")] string productId,
        CancellationToken token = default)
    {
        var _ = httpContext
            .Username()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var result = await productService.GetProductVariantsAsync(productId, token);

        return TypedResults.Ok(result);
    }
}
