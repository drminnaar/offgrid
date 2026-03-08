using System.Text.Json.Serialization;

namespace Offgrid.Portal.ProductSearch.Domain.Entities;

/// <summary>
/// Represents a document for indexing a product in the search system.
/// </summary>
public sealed class ProductSearchDocument
{
    // identity fields

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("productId")]
    public required string ProductId { get; init; }

    [JsonPropertyName("productSku")]
    public required string ProductSku { get; init; }

    [JsonPropertyName("variantSku")]
    public required string VariantSku { get; init; }

    // Searchable fields

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("variantName")]
    public required string VariantName { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("brand")]
    public required string Brand { get; init; }

    [JsonPropertyName("category")]
    public required string Category { get; init; }

    [JsonPropertyName("subcategory")]
    public required string Subcategory { get; init; }

    [JsonPropertyName("features")]
    public required string[] Features { get; init; }

    // pricing fields

    [JsonPropertyName("isOnSale")]
    public required bool IsOnSale { get; init; }

    [JsonPropertyName("salePercentage")]
    public required int SalePercentage { get; init; }

    [JsonPropertyName("basePrice")]
    public required decimal BasePrice { get; init; }

    [JsonPropertyName("currentPrice")]
    public required decimal CurrentPrice { get; init; }

    // specifications

    [JsonPropertyName("specifications")]
    public Dictionary<string, object>? Specifications { get; init; }

    // variant facets

    [JsonPropertyName("color")]
    public required string Color { get; init; }

    [JsonPropertyName("colorHex")]
    public required string ColorHex { get; init; }

    [JsonPropertyName("size")]
    public string? Size { get; init; }

    [JsonPropertyName("package")]
    public string? Package { get; init; }

    [JsonPropertyName("buildKit")]
    public string? BuildKit { get; init; }

    [JsonPropertyName("finSetup")]
    public string? FinSetup { get; init; }

    // stock

    [JsonPropertyName("totalStock")]
    public required int TotalStock { get; init; }

    [JsonPropertyName("hasStock")]
    public required bool HasStock { get; init; }

    // sorting & metadata

    [JsonPropertyName("createdAtUnixTimeSeconds")]
    public long? CreatedAtUnixTimeSeconds { get; init; }

    [JsonPropertyName("updatedAtUnixTimeSeconds")]
    public required long UpdatedAtUnixTimeSeconds { get; init; }

    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; init; }
}
