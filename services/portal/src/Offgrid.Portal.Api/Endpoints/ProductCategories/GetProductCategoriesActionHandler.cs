using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.Products.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.ProductCategories;

public static class GetProductCategoriesActionHandler
{
    public const string EndpointName = "GetProductCategories";

    public static async Task<IResult> GetProductCategoriesAsync(
        [FromServices] IProductService productService,
        CancellationToken token = default)
    {
        var result = await productService.GetProductCategoriesAsync(token);

        return TypedResults.Ok(result);
    }
}
