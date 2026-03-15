namespace Offgrid.Shop.Products.Application.Services;

public interface IProductSearchService
{
    Task<ProductSearchResult> SearchAsync(ProductSearchCriteria criteria, CancellationToken cancellationToken = default);
}
