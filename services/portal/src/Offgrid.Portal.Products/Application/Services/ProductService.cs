using Offgrid.Framework.Domain;
using Offgrid.Portal.Products.Application.Queries.GetProductBrands;
using Offgrid.Portal.Products.Application.Queries.GetProductCategories;
using Offgrid.Portal.Products.Application.Queries.GetProducts;
using Offgrid.Portal.Products.Application.Queries.GetProductTypes;

namespace Offgrid.Portal.Products.Application.Services;

public interface IProductService
{
    public const string CollectionName = "products";
    Task<PagedListResult<ProductInfo>> GetProductsAsync(GetProductsQuery query, CancellationToken cancellationToken = default);
    Task<List<string>> GetProductBrandsAsync(CancellationToken cancellationToken = default);
    Task<List<CategoryInfo>> GetProductCategoriesAsync(CancellationToken cancellationToken = default);
    Task<List<string>> GetProductTypesAsync(CancellationToken cancellationToken = default);
}

public sealed class ProductService : IProductService
{
    private readonly IGetProductsHandler _getProductsHandler;
    private readonly IGetProductTypesHandler _getProductTypesHandler;
    private readonly IGetProductCategoriesHandler _getProductCategoriesHandler;
    private readonly IGetProductBrandsHandler _getProductBrandsHandler;

    public ProductService(
        IGetProductsHandler getProductsHandler,
        IGetProductTypesHandler getProductTypesHandler,
        IGetProductCategoriesHandler getProductCategoriesHandler,
        IGetProductBrandsHandler getProductBrandsHandler)
    {
        ArgumentNullException.ThrowIfNull(getProductsHandler, nameof(getProductsHandler));
        ArgumentNullException.ThrowIfNull(getProductTypesHandler, nameof(getProductTypesHandler));
        ArgumentNullException.ThrowIfNull(getProductCategoriesHandler, nameof(getProductCategoriesHandler));
        ArgumentNullException.ThrowIfNull(getProductBrandsHandler, nameof(getProductBrandsHandler));

        _getProductsHandler = getProductsHandler;
        _getProductTypesHandler = getProductTypesHandler;
        _getProductCategoriesHandler = getProductCategoriesHandler;
        _getProductBrandsHandler = getProductBrandsHandler;
    }

    public async Task<PagedListResult<ProductInfo>> GetProductsAsync(
        GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        return await _getProductsHandler.HandleAsync(query, cancellationToken);
    }

    public async Task<List<string>> GetProductBrandsAsync(CancellationToken cancellationToken = default)
    {
        return await _getProductBrandsHandler.HandleAsync(IProductService.CollectionName, cancellationToken);
    }

    public async Task<List<CategoryInfo>> GetProductCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _getProductCategoriesHandler.HandleAsync(IProductService.CollectionName, cancellationToken);
    }

    public async Task<List<string>> GetProductTypesAsync(CancellationToken cancellationToken = default)
    {
        return await _getProductTypesHandler.HandleAsync(IProductService.CollectionName, cancellationToken);
    }
}
