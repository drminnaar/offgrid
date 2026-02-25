using MongoDB.Driver;
using Offgrid.Framework.Exceptions;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProductVariants;

public sealed class GetProductVariantsQueryHandler
{
    private readonly IMongoCollectionProvider _collectionProvider;

    public GetProductVariantsQueryHandler(IMongoCollectionProvider collectionProvider)
    {
        ArgumentNullException.ThrowIfNull(collectionProvider, nameof(collectionProvider));
        _collectionProvider = collectionProvider;
    }

    public async Task<IReadOnlyList<ProductVariantInfo>> HandleAsync(
        string collectionName,
        string productId,
        CancellationToken cancellationToken)
    {
        var collection = _collectionProvider.GetCollection<Product>(collectionName);
        var filter = Builders<Product>.Filter.Eq(p => p.ProductId, productId);
        var cursor = await collection.FindAsync(filter, cancellationToken: cancellationToken);
        var product = await cursor.FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            throw new EntityNotFoundException($"Product with ID '{productId}' not found.");
        }

        return product.Variants.Select(v => v.ToProductVariantInfo()).ToList();
    }
}
