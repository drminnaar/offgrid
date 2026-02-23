namespace Offgrid.Portal.Products.Application.Queries.GetProductCategories;

public sealed record CategoryInfo
{
    public string Category { get; init; } = string.Empty;
    public string[] Subcategories { get; init; } = [];
}
