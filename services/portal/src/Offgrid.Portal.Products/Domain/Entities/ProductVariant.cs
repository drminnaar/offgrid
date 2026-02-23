using MongoDB.Bson.Serialization.Attributes;

namespace Offgrid.Portal.Products.Domain.Entities;

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
}
