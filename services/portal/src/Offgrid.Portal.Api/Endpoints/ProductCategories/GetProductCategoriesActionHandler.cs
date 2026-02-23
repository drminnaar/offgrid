using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.ProductCategories;

public static class GetProductCategoriesActionHandler
{
    public const string EndpointName = "GetProductCategories";

    public static async Task<IResult> GetProductCategoriesAsync(
        [FromServices] IProductService productService,
        HttpContext httpContext,
        CancellationToken token = default)
    {
        var _ = httpContext
            .Username()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var result = await productService.GetProductCategoriesAsync(token);

        return TypedResults.Ok(result);
    }
}
