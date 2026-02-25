using Offgrid.Framework.Domain;
using Offgrid.Portal.Products.Application.Queries.GetProductBrands;
using Offgrid.Portal.Products.Application.Queries.GetProductById;
using Offgrid.Portal.Products.Application.Queries.GetProductCategories;
using Offgrid.Portal.Products.Application.Queries.GetProducts;
using Offgrid.Portal.Products.Application.Queries.GetProductTypes;
using Offgrid.Portal.Products.Application.Queries.GetProductVariants;

namespace Offgrid.Portal.Products.Application.Services;

public interface IProductService
{
    public const string CollectionName = "products";
    Task<ProductDetail> GetProductByIdAsync(string productId, CancellationToken cancellationToken = default);
    Task<PagedListResult<ProductInfo>> GetProductsAsync(GetProductsQuery query, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetProductBrandsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CategoryInfo>> GetProductCategoriesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetProductTypesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductVariantInfo>> GetProductVariantsAsync(string productId, CancellationToken cancellationToken = default);
}

public sealed class ProductService : IProductService
{
    private readonly IGetProductsHandler _getProductsHandler;
    private readonly IGetProductTypesHandler _getProductTypesHandler;
    private readonly IGetProductCategoriesHandler _getProductCategoriesHandler;
    private readonly IGetProductBrandsHandler _getProductBrandsHandler;
    private readonly IGetProductByIdHandler _getProductByIdHandler;
    private readonly GetProductVariantsQueryHandler _getProductVariantsHandler;

    public ProductService(
        IGetProductsHandler getProductsHandler,
        IGetProductTypesHandler getProductTypesHandler,
        IGetProductCategoriesHandler getProductCategoriesHandler,
        IGetProductBrandsHandler getProductBrandsHandler,
        IGetProductByIdHandler getProductByIdHandler,
        GetProductVariantsQueryHandler getProductVariantsHandler)
    {
        ArgumentNullException.ThrowIfNull(getProductsHandler, nameof(getProductsHandler));
        ArgumentNullException.ThrowIfNull(getProductTypesHandler, nameof(getProductTypesHandler));
        ArgumentNullException.ThrowIfNull(getProductCategoriesHandler, nameof(getProductCategoriesHandler));
        ArgumentNullException.ThrowIfNull(getProductBrandsHandler, nameof(getProductBrandsHandler));
        ArgumentNullException.ThrowIfNull(getProductByIdHandler, nameof(getProductByIdHandler));
        ArgumentNullException.ThrowIfNull(getProductVariantsHandler, nameof(getProductVariantsHandler));

        _getProductsHandler = getProductsHandler;
        _getProductTypesHandler = getProductTypesHandler;
        _getProductCategoriesHandler = getProductCategoriesHandler;
        _getProductBrandsHandler = getProductBrandsHandler;
        _getProductByIdHandler = getProductByIdHandler;
        _getProductVariantsHandler = getProductVariantsHandler;
    }

    public async Task<ProductDetail> GetProductByIdAsync(string productId, CancellationToken cancellationToken = default)
    {
        return await _getProductByIdHandler.HandleAsync(IProductService.CollectionName, productId, cancellationToken);
    }

    public async Task<PagedListResult<ProductInfo>> GetProductsAsync(
        GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        return await _getProductsHandler.HandleAsync(query, cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetProductBrandsAsync(CancellationToken cancellationToken = default)
    {
        return await _getProductBrandsHandler.HandleAsync(IProductService.CollectionName, cancellationToken);
    }

    public async Task<IReadOnlyList<CategoryInfo>> GetProductCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _getProductCategoriesHandler.HandleAsync(IProductService.CollectionName, cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetProductTypesAsync(CancellationToken cancellationToken = default)
    {
        return await _getProductTypesHandler.HandleAsync(IProductService.CollectionName, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductVariantInfo>> GetProductVariantsAsync(
        string productId, CancellationToken cancellationToken = default)
    {
        return await _getProductVariantsHandler.HandleAsync(IProductService.CollectionName, productId, cancellationToken);
    }
}
