namespace Offgrid.Portal.Products.Application.Queries.GetProducts;

public sealed record GetProductsQuery
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;

    public int Page { get; init; } = DefaultPageNumber;

    public int PageSize { get; init; } = DefaultPageSize;

    public string Brands { get; set; } = string.Empty;

    public string Categories { get; set; } = string.Empty;

    public string Types { get; set; } = string.Empty;

    public string[] GetBrandList() => string.IsNullOrWhiteSpace(Brands) ? Array.Empty<string>() : Brands
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(b => b.ToLowerInvariant())
        .Distinct()
        .ToArray();

    public string[] GetCategoryList() => string.IsNullOrWhiteSpace(Categories) ? Array.Empty<string>() : Categories
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(c => c.ToLowerInvariant())
        .Distinct()
        .ToArray();

    public string[] GetTypeList() => string.IsNullOrWhiteSpace(Types) ? Array.Empty<string>() : Types
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(t => t.ToLowerInvariant())
        .Distinct()
        .ToArray();
}
