using Offgrid.Framework.Domain.Extensions;
using Offgrid.Framework.System.Collections.Generic;
using Offgrid.Shop.Products.Application.Services;
using Offgrid.Shop.Products.Domain.Entities;
using Typesense;

namespace Offgrid.Shop.Products.Infrastructure.Search;

public class TypesenseProductSearchService : IProductSearchService
{
    private readonly ITypesenseClient _client;

    public TypesenseProductSearchService(ITypesenseClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));
        _client = client;
    }

    public async Task<ProductSearchResult> SearchAsync(
        ProductSearchCriteria criteria,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(criteria.Page, 1, nameof(criteria.Page));
        ArgumentOutOfRangeException.ThrowIfLessThan(criteria.PageSize, 1, nameof(criteria.PageSize));

        var searchParameters = new SearchParameters(
            text: criteria.Query ?? "*",
            queryBy: "name,description,brand,category,subcategory,features")
        {
            Page = criteria.Page,
            PerPage = criteria.PageSize,
            FacetBy = "type,category,subcategory,brand,color,size,isOnSale"
        };

        if (!string.IsNullOrWhiteSpace(criteria.SortBy))
        {
            searchParameters.SortBy = criteria.SortBy;
        }

        var filterBy = BuildFilterBy(criteria);

        if (!string.IsNullOrWhiteSpace(filterBy))
        {
            searchParameters.FilterBy = filterBy;
        }

        var searchResult = await _client.Search<ProductSearchDocument>(
            "products",
            searchParameters,
            cancellationToken);

        var items = searchResult
            .Hits?
            .Select(hit => hit.Document)
            .Where(doc => doc != null)
            .Cast<ProductSearchDocument>()
            .ToList() ?? [];

        var totalCount = searchResult.Found;

        var pagedList = new PagedList<ProductSearchDocument>(
            items,
            totalCount,
            criteria.Page,
            criteria.PageSize);

        var facets = MapFacets(searchResult.FacetCounts);

        var facetResults = MapFacets(searchResult);
        return new ProductSearchResult(
            pagedList.ToList(),
            new PageMetadata(
                CurrentPageNumber: pagedList.CurrentPageNumber,
                PageSize: pagedList.PageSize,
                ItemCount: pagedList.ItemCount,
                PageCount: pagedList.PageCount,
                LastPageNumber: pagedList.LastPageNumber,
                NextPageNumber: pagedList.NextPageNumber,
                PreviousPageNumber: pagedList.PreviousPageNumber,
                HasNext: pagedList.HasNext,
                HasPrevious: pagedList.HasPrevious),
            facets);
    }

    private static string? BuildFilterBy(ProductSearchCriteria? criteria)
    {
        if (criteria is null)
        {
            return default;
        }

        var filters = new List<string>();

        AddInFilter(filters, "type", criteria.Types);
        AddInFilter(filters, "category", criteria.Categories);
        AddInFilter(filters, "subcategory", criteria.Subcategories);
        AddInFilter(filters, "brand", criteria.Brands);
        AddInFilter(filters, "color", criteria.Colors);
        AddInFilter(filters, "size", criteria.Sizes);

        if (criteria.OnSaleOnly.HasValue)
        {
            filters.Add($"isOnSale:{criteria.OnSaleOnly.Value.ToString().ToLowerInvariant()}");
        }

        if (criteria.InStockOnly.HasValue)
        {
            filters.Add($"hasStock:{criteria.InStockOnly.Value.ToString().ToLowerInvariant()}");
        }

        if (criteria.MinPrice.HasValue)
        {
            filters.Add($"currentPrice:>={criteria.MinPrice.Value}");
        }

        if (criteria.MaxPrice.HasValue)
        {
            filters.Add($"currentPrice:<={criteria.MaxPrice.Value}");
        }

        return filters.Count > 0
            ? string.Join(" && ", filters)
            : default;
    }

    private static void AddInFilter(List<string> filters, string fieldName, IReadOnlyList<string>? values)
    {
        if (values is null || values.Count == 0)
        {
            return;
        }

        var escapedValues = values
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => $"`{EscapeFilterValue(value.Trim())}`")
            .ToArray();

        if (escapedValues.Length == 0)
        {
            return;
        }

        filters.Add($"{fieldName}:[{string.Join(",", escapedValues)}]");
    }

    private static string EscapeFilterValue(string value)
        => value.Replace("`", "\\`");

    private static ProductSearchFacets MapFacets(IReadOnlyCollection<Typesense.FacetCount>? facetCounts)
    {
        if (facetCounts is null || facetCounts.Count == 0)
        {
            return new ProductSearchFacets(
                Types: [],
                Categories: [],
                Subcategories: [],
                Brands: [],
                Colors: [],
                Sizes: [],
                IsOnSale: []);
        }

        return new ProductSearchFacets(
            Types: GetFacetCounts(facetCounts, "type"),
            Categories: GetFacetCounts(facetCounts, "category"),
            Subcategories: GetFacetCounts(facetCounts, "subcategory"),
            Brands: GetFacetCounts(facetCounts, "brand"),
            Colors: GetFacetCounts(facetCounts, "color"),
            Sizes: GetFacetCounts(facetCounts, "size"),
            IsOnSale: GetFacetCounts(facetCounts, "isOnSale"));
    }

    private static IReadOnlyList<Application.Services.FacetCount> GetFacetCounts(
        IReadOnlyCollection<Typesense.FacetCount> facetCounts,
        string fieldName)
    {
        var facet = facetCounts.FirstOrDefault(f => f.FieldName == fieldName);

        if (facet?.Counts is null || facet.Counts.Count == 0)
        {
            return [];
        }

        return facet.Counts
            .Select(c => new Application.Services.FacetCount(c.Value ?? string.Empty, c.Count))
            .ToList();
    }


    // ----------------------------------------------------------------------------------------------
    private static readonly IReadOnlyDictionary<string, string> FacetLabels =
        new Dictionary<string, string>
        {
            ["category"] = "Category",
            ["subcategory"] = "Subcategory",
            ["brand"] = "Brand",
            ["type"] = "Type",
            ["color"] = "Colour",
            ["size"] = "Size",
            ["finSetup"] = "Fin Setup",
            ["buildKit"] = "Build Kit",
            ["package"] = "Package",
            ["isOnSale"] = "On Sale",
            ["hasStock"] = "In Stock",
        };

    private static FacetResultsDto MapFacets(SearchResult<ProductSearchDocument> result)
    {
        if (result.FacetCounts is null) return new FacetResultsDto([]);

        var groups = result.FacetCounts
            .Select(fc => new FacetGroupDto(
                Field: fc.FieldName,
                Label: FacetLabels.GetValueOrDefault(fc.FieldName, fc.FieldName),
                Values: fc.Counts
                          .Select(c => new FacetValueDto(c.Value, c.Count))
                          .ToList()
            ))
            .ToList();

        return new FacetResultsDto(groups);
    }
}
