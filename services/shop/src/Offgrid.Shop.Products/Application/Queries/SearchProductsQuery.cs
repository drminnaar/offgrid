namespace Offgrid.Shop.Products.Application.Queries;

public sealed record SearchProductsQuery
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string QueryText { get; init; } = string.Empty;
}
