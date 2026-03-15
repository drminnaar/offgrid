using Offgrid.Shop.Products.Domain.Entities;

namespace Offgrid.Shop.Products.Domain.Mappers;

public static partial class MapExtensions
{
    /// <summary>
    /// Maps a Product entity to a list of ProductSearchDocument instances, one for each variant
    /// of the product. Each ProductSearchDocument contains information from the Product as well
    /// as the specific variant, allowing for detailed search indexing.   
    /// </summary>
    /// <param name="product">The Product entity to be mapped to search documents.</param>
    /// <returns>A list of ProductSearchDocument instances representing the product and its variants.</returns>
    public static IReadOnlyList<ProductSearchDocument> ToProductSearchDocuments(this Product product)
    {
        var searchDocuments = new List<ProductSearchDocument>();
        foreach (var variant in product.Variants)
        {
            // Map each variant to a ProductSearchDocument
            var variantSearchDocument = new ProductSearchDocument
            {
                CreatedAtUnixTimeSeconds = product.CreatedAtUnixTimeSeconds,
                UpdatedAtUnixTimeSeconds = product.UpdatedAtUnixTimeSeconds,
                Id = $"{product.Id}-{variant.Sku}",
                ProductId = product.ProductId,
                ProductSku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                IsOnSale = product.IsOnSale,
                SalePercentage = product.SalePercentage,
                BasePrice = product.BasePrice + variant.PriceModifier,
                CurrentPrice = product.CurrentPrice + variant.PriceModifier,
                Brand = product.Brand,
                Type = product.Type,
                Category = product.Category,
                Subcategory = product.Subcategory,
                Features = [.. product.Features],
                VariantSku = variant.Sku,
                VariantName = variant.Name,
                HasStock = variant.HasStock,
                TotalStock = variant.StockQuantity,
                ImageUrl = variant.ImageUrl,
                Color = variant.Attributes.TryGetValue("color", out var color) ? color : string.Empty,
                ColorHex = variant.Attributes.TryGetValue("colorHex", out var colorHex) ? colorHex : string.Empty,
                BuildKit = variant.Attributes.TryGetValue("buildKit", out var buildKit) ? buildKit : string.Empty,
                FinSetup = variant.Attributes.TryGetValue("finSetup", out var finSetup) ? finSetup : string.Empty,
                Package = variant.Attributes.TryGetValue("package", out var package) ? package : string.Empty,
                Size = variant.Attributes.TryGetValue("size", out var size) ? size : string.Empty,
                Specifications = product.Specifications.ToDictionary(kv => kv.Key, kv => (object)kv.Value),
            };
            searchDocuments.Add(variantSearchDocument);
        }
        return searchDocuments;
    }
}
