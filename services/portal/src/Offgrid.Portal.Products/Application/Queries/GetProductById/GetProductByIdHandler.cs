using MongoDB.Driver;
using Offgrid.Framework.Exceptions;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProductById;

public interface IGetProductByIdHandler
{
    Task<ProductDetail> HandleAsync(string collectionName, string productId, CancellationToken cancellationToken);
}

public sealed class GetProductByIdHandler : IGetProductByIdHandler
{
    private readonly IMongoCollectionProvider _collectionProvider;

    public GetProductByIdHandler(IMongoCollectionProvider collectionProvider)
    {
        ArgumentNullException.ThrowIfNull(collectionProvider, nameof(collectionProvider));
        _collectionProvider = collectionProvider;
    }

    public async Task<ProductDetail> HandleAsync(string collectionName, string productId, CancellationToken cancellationToken)
    {
        var collection = _collectionProvider.GetCollection<Product>(collectionName);
        var filter = Builders<Product>.Filter.Eq(e => e.ProductId, productId);
        var productCursor = await collection.FindAsync(filter, cancellationToken: cancellationToken);
        var product = await productCursor.FirstOrDefaultAsync(cancellationToken);
        return product
            ?.ToProductDetail()
            ?? throw new EntityNotFoundException($"Product with ID {productId} not found.")
            {
                EntityKey = productId.ToString(),
                EntityType = nameof(Product)
            };
    }
}
