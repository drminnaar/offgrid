using MongoDB.Bson.Serialization.Attributes;

namespace Offgrid.Portal.ProductSearch.Domain.Entities;

/// <summary>
/// Represents a variant of a product in the product catalog.
/// </summary>
public sealed class ProductVariant
{
    [BsonElement("sku")]
    public required string Sku { get; set; }

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("priceModifier")]
    public decimal PriceModifier { get; set; }

    [BsonElement("attributes")]
    public Dictionary<string, string> Attributes { get; set; } = [];

    [BsonElement("stockQuantity")]
    public int StockQuantity { get; set; }

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [BsonIgnore]
    public bool HasStock => StockQuantity > 0;
}
