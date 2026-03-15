using Offgrid.Shop.Products.Application.Services;

namespace Offgrid.Shop.Products.Application.Queries;

public interface ISearchProductsQueryHandler
{
    Task<ProductSearchResult> SearchAsync(SearchProductsQuery query, CancellationToken cancellationToken = default);
}

public sealed class SearchProductsQueryHandler : ISearchProductsQueryHandler
{
    private readonly IProductSearchService _repository;

    public SearchProductsQueryHandler(IProductSearchService repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        _repository = repository;
    }

    public async Task<ProductSearchResult> SearchAsync(
        SearchProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.SearchAsync(
            new ProductSearchCriteria
            {
                Query = query.QueryText,
                Page = query.PageNumber,
                PageSize = query.PageSize
            },
            cancellationToken);
        return result;
    }
}
