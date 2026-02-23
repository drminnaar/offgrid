namespace Offgrid.Portal.Products.Application.Queries.GetProducts;

public sealed record ProductInfo
{
    public required string Id { get; init; }
    public required string ProductId { get; init; }
    public required string Sku { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Brand { get; init; }
    public required string Type { get; init; }
    public required string Category { get; init; }
    public required string Subcategory { get; init; }
    public required string PrimaryImageUrl { get; init; }
    public required int TotalStockQuantity { get; init; }
    public required string StockLevel { get; init; }
    public required decimal BasePrice { get; init; }
    public required decimal CurrentPrice { get; init; }
    public required bool IsOnSale { get; init; }
    public required int SalePercentage { get; init; }
    public required long CreatedAtUnixTimeSeconds { get; init; }
    public required long UpdatedAtUnixTimeSeconds { get; init; }
}
