namespace Offgrid.Shop.Products.Application.Services;

/// <summary>
/// Represents optional search filters provided by the API for product search.
/// </summary>
public sealed record ProductSearchCriteria
{
    /// <summary>Full-text query. Omit or pass "*" to match all documents.</summary>
    public string? Query { get; init; }

    /// <summary>1-based page number (default: 1).</summary>
    public int Page { get; init; } = 1;

    /// <summary>Items per page, clamped to 1–100 (default: 20).</summary>
    public int PageSize { get; init; } = 20;

    // ── Sorting ────────────────────────────────────────────────────────

    /// <summary>
    /// Allowed values: "currentPrice:asc", "currentPrice:desc".
    /// Null = relevance (Typesense default).
    /// </summary>
    public string? SortBy { get; init; }

    // ── Facet filters ────────────────────────────────────────────────────────

    public IReadOnlyList<string> Categories { get; init; } = [];
    public IReadOnlyList<string> Subcategories { get; init; } = [];
    public IReadOnlyList<string> Brands { get; init; } = [];
    public IReadOnlyList<string> Types { get; init; } = [];
    public IReadOnlyList<string> Colors { get; init; } = [];
    public IReadOnlyList<string> Sizes { get; init; } = [];

    // ── Price range ──────────────────────────────────────────────────────────

    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }

    // ── Boolean toggles ──────────────────────────────────────────────────────

    public bool? InStockOnly { get; init; }
    public bool? OnSaleOnly { get; init; }
}
