using MongoDB.Driver;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProductTypes;

public interface IGetProductTypesHandler
{
    Task<List<string>> HandleAsync(string collectionName, CancellationToken cancellationToken = default);
}

public sealed class GetProductTypesHandler : IGetProductTypesHandler
{
    private readonly IMongoCollectionProvider _collectionProvider;

    public GetProductTypesHandler(IMongoCollectionProvider collectionProvider)
    {
        ArgumentNullException.ThrowIfNull(collectionProvider, nameof(collectionProvider));
        _collectionProvider = collectionProvider;
    }

    public async Task<List<string>> HandleAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var collection = _collectionProvider.GetCollection<Product>(collectionName);

        return await collection.Distinct<string>(
            "type",
            FilterDefinition<Product>.Empty)
            .ToListAsync();
    }
}
