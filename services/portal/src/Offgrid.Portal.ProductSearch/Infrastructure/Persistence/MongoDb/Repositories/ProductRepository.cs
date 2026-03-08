using System.Runtime.CompilerServices;
using MongoDB.Driver;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Domain.Services;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Persistence.MongoDb.Repositories;

public sealed class ProductRepository : IProductCatalog
{
    private readonly IMongoCollectionProvider _collectionProvider;

    public ProductRepository(IMongoCollectionProvider collectionProvider)
    {
        ArgumentNullException.ThrowIfNull(collectionProvider, nameof(collectionProvider));
        _collectionProvider = collectionProvider;
    }

    public async Task<IReadOnlyList<Product>> GetAvailableProductsAsync(CancellationToken cancellationToken = default)
    {
        return await StreamAvailableProductsAsync(cancellationToken).ToListAsync(cancellationToken);
    }

    public async IAsyncEnumerable<Product> StreamAvailableProductsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var collection = _collectionProvider.GetCollection<Product>("products");
        var filter = Builders<Product>.Filter.Empty;
        var cursor = await collection.FindAsync(filter, cancellationToken: cancellationToken);
        while (await cursor.MoveNextAsync(cancellationToken))
        {
            foreach (var product in cursor.Current)
            {
                yield return product;
            }
        }
    }
}
