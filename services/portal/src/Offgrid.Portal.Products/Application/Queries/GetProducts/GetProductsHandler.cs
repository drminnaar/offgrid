using MongoDB.Driver;
using Offgrid.Framework.Domain;
using Offgrid.Framework.Domain.Extensions;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProducts;

public interface IGetProductsHandler
{
    Task<PagedListResult<ProductInfo>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken);
}

public sealed class GetProductsHandler : IGetProductsHandler
{
    private readonly IMongoRepository<Product> _repository;

    public GetProductsHandler(IMongoRepository<Product> repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        _repository = repository;
    }

    public async Task<PagedListResult<ProductInfo>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Empty;
        var sort = Builders<Product>.Sort.Ascending(p => p.UpdatedAtUnixTimeSeconds);
        var products = await _repository.FindAsync(query, filter, sort, cancellationToken);
        return products.ToPagedListResult(product => product.ToProductInfo());
    }
}
