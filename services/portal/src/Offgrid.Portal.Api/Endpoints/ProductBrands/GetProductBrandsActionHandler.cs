using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.ProductBrands;

public static class GetProductBrandsActionHandler
{
    public const string EndpointName = "GetProductBrands";

    public static async Task<IResult> GetProductBrandsAsync(
        [FromServices] IProductService productService,
        HttpContext httpContext,
        CancellationToken token = default)
    {
        var _ = httpContext
            .Username()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var result = await productService.GetProductBrandsAsync(token);

        return TypedResults.Ok(result);
    }
}
