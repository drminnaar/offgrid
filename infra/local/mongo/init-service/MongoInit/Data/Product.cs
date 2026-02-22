using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoInit.Data;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required ObjectId Id { get; set; }

    [BsonElement("productId")]
    public string ProductId { get; set; } = string.Empty;

    [BsonElement("sku")]
    public string Sku { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("isOnSale")]
    public bool IsOnSale { get; set; }

    [BsonElement("salePercentage")]
    public int SalePercentage { get; set; }

    [BsonElement("basePrice")]
    public decimal BasePrice { get; set; }

    [BsonElement("currentPrice")]
    public decimal CurrentPrice { get; set; }

    [BsonElement("brand")]
    public string Brand { get; set; } = string.Empty;

    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;

    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;

    [BsonElement("subcategory")]
    public string Subcategory { get; set; } = string.Empty;

    [BsonElement("updatedAtUnixTimeSeconds")]
    public long UpdatedAtUnixTimeSeconds { get; set; }

    [BsonElement("features")]
    public List<string> Features { get; set; } = [];

    [BsonElement("specifications")]
    public Dictionary<string, string> Specifications { get; set; } = [];

    [BsonElement("variants")]
    public List<ProductVariant> Variants { get; set; } = [];

    [BsonElement("primaryImageUrl")]
    public required string PrimaryImageUrl { get; set; }

    [BsonElement("imageUrls")]
    public required List<string> ImageUrls { get; set; }
}
