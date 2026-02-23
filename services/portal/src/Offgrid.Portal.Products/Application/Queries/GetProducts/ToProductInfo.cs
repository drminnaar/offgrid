using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProducts;

public static class GetProductsExtensions
{
    public static ProductInfo ToProductInfo(this Product product) => new()
    {
        BasePrice = product.BasePrice,
        CreatedAtUnixTimeSeconds = product.CreatedAtUnixTimeSeconds,
        CurrentPrice = product.CurrentPrice,
        Description = product.Description,
        Id = product.Id.ToString(),
        Name = product.Name,
        ProductId = product.ProductId.ToString(),
        Sku = product.Sku,
        StockLevel = product.StockLevel,
        TotalStockQuantity = product.TotalStockQuantity,
        IsOnSale = product.IsOnSale,
        PrimaryImageUrl = product.PrimaryImageUrl,
        SalePercentage = product.SalePercentage,
        Brand = product.Brand,
        Type = product.Type,
        Category = product.Category,
        Subcategory = product.Subcategory,
        UpdatedAtUnixTimeSeconds = product.UpdatedAtUnixTimeSeconds,
    };
}
