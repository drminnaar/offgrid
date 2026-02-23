using MongoDB.Driver;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProductBrands;

public interface IGetProductBrandsHandler
{
    Task<List<string>> HandleAsync(string collectionName, CancellationToken cancellationToken = default);
}

public sealed class GetProductBrandsHandler : IGetProductBrandsHandler
{
    private readonly IMongoCollectionProvider _collectionProvider;

    public GetProductBrandsHandler(IMongoCollectionProvider collectionProvider)
    {
        ArgumentNullException.ThrowIfNull(collectionProvider, nameof(collectionProvider));
        _collectionProvider = collectionProvider;
    }

    public async Task<List<string>> HandleAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var collection = _collectionProvider.GetCollection<Product>(collectionName);

        return await collection.Distinct<string>(
            "brand",
            FilterDefinition<Product>.Empty)
            .ToListAsync();
    }
}
