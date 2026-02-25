namespace Offgrid.Portal.Products.Application.Queries.GetProductVariants;

public sealed record ProductVariantInfo
{
    public required string Sku { get; init; }

    public required string Name { get; init; }

    public decimal PriceModifier { get; init; }

    public Dictionary<string, string> Attributes { get; init; } = [];

    public int StockQuantity { get; init; }

    public string ImageUrl { get; init; } = string.Empty;
}
