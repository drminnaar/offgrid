using Microsoft.AspNetCore.Mvc;
using Offgrid.Shop.Products.Application.Services;

namespace Offgrid.Shop.Api.Endpoints.Products;

public sealed class SearchProductsHandler
{
    public const string EndpointName = "SearchProducts";

    public static async Task<IResult> SearchProductsAsync(
        [FromServices] IProductSearchService productSearchService,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string query = "",
        [FromQuery] string? sortBy = null,
        [FromQuery] string[]? categories = null,
        [FromQuery] string[]? subcategories = null,
        [FromQuery] string[]? brands = null,
        [FromQuery] string[]? types = null,
        [FromQuery] string[]? colors = null,
        [FromQuery] string[]? sizes = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] bool? inStockOnly = null,
        [FromQuery] bool? onSaleOnly = null,
        CancellationToken token = default)
    {
        var criteria = new ProductSearchCriteria
        {
            Query = query,
            Page = page,
            PageSize = pageSize,
            SortBy = sortBy,
            Categories = categories ?? [],
            Subcategories = subcategories ?? [],
            Brands = brands ?? [],
            Types = types ?? [],
            Colors = colors ?? [],
            Sizes = sizes ?? [],
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            OnSaleOnly = onSaleOnly,
        };
        var result = await productSearchService.SearchAsync(criteria, token);
        return TypedResults.Ok(result);
    }
}
