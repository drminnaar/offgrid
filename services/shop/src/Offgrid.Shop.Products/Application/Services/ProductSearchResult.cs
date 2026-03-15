using Offgrid.Framework.Domain;
using Offgrid.Shop.Products.Domain.Entities;

namespace Offgrid.Shop.Products.Application.Services;

/// <summary>
/// Search response with paged documents and facet counts.
/// </summary>
public sealed record ProductSearchResult(
    IReadOnlyList<ProductSearchDocument> Items,
    PageMetadata PageMetadata,
    ProductSearchFacets Facets);

/// <summary>
/// Facet groups returned by the search engine.
/// </summary>
public sealed record ProductSearchFacets(
    IReadOnlyList<FacetCount> Types,
    IReadOnlyList<FacetCount> Categories,
    IReadOnlyList<FacetCount> Subcategories,
    IReadOnlyList<FacetCount> Brands,
    IReadOnlyList<FacetCount> Colors,
    IReadOnlyList<FacetCount> Sizes,
    IReadOnlyList<FacetCount> IsOnSale);

/// <summary>
/// One facet value and its document count.
/// </summary>
public sealed record FacetCount(
    string Value,
    long Count);


public sealed record PageMetadata(
    int CurrentPageNumber,
    int PageSize,
    long ItemCount,
    int PageCount,
    int LastPageNumber,
    int? NextPageNumber,
    int? PreviousPageNumber,
    bool HasNext,
    bool HasPrevious);


public sealed record FacetResultsDto(IReadOnlyList<FacetGroupDto> Groups);

public sealed record FacetGroupDto(
    string Field,
    string Label,
    IReadOnlyList<FacetValueDto> Values
);

public sealed record FacetValueDto(string Value, long Count);
