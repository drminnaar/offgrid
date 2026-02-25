using MongoDB.Bson;
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
        if (query.GetBrandList().Length > 0)
        {
            var brands = query.GetBrandList();
            filter &= Builders<Product>.Filter.Regex(p => p.Brand, new BsonRegularExpression(string.Join("|", brands), "i"));
        }
        if (query.GetCategoryList().Length > 0)
        {
            var categories = query.GetCategoryList();
            filter &= Builders<Product>.Filter.Regex(p => p.Category, new BsonRegularExpression(string.Join("|", categories), "i"));
        }
        if (query.GetTypeList().Length > 0)
        {
            var types = query.GetTypeList();
            filter &= Builders<Product>.Filter.Regex(p => p.Type, new BsonRegularExpression(string.Join("|", types), "i"));
        }
        var options = new QueryOptions<Product>
        {
            SortDefinition = Builders<Product>.Sort.Descending(p => p.UpdatedAtUnixTimeSeconds),
            Page = query.Page,
            PageSize = query.PageSize
        };
        var products = await _repository.FindAsync(options, filter, cancellationToken);
        return products.ToPagedListResult(product => product.ToProductInfo());
    }
}
