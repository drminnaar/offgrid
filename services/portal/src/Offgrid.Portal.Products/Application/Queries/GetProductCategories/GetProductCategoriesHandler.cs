using MongoDB.Bson;
using MongoDB.Driver;
using Offgrid.Framework.MongoDb;
using Offgrid.Portal.Products.Domain.Entities;

namespace Offgrid.Portal.Products.Application.Queries.GetProductCategories;

public interface IGetProductCategoriesHandler
{
    Task<List<CategoryInfo>> HandleAsync(string collectionName, CancellationToken cancellationToken = default);
}

public sealed class GetProductCategoriesHandler : IGetProductCategoriesHandler
{
    private readonly IMongoCollectionProvider _collectionProvider;

    public GetProductCategoriesHandler(IMongoCollectionProvider collectionProvider)
    {
        ArgumentNullException.ThrowIfNull(collectionProvider, nameof(collectionProvider));
        _collectionProvider = collectionProvider;
    }

    public async Task<List<CategoryInfo>> HandleAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        var collection = _collectionProvider.GetCollection<Product>(collectionName);
        var pipeline = CreatePipeline();
        var cursor = await collection.AggregateAsync<BsonDocument>(pipeline, cancellationToken: cancellationToken);
        var documents = await cursor.ToListAsync(cancellationToken);
        return documents.Select(doc => new CategoryInfo
        {
            Category = doc["category"].AsString,
            Subcategories = doc["subcategories"].AsBsonArray.Select(x => x.AsString).ToArray()
        }).ToList();
    }

    private static BsonDocument[] CreatePipeline() => new[]
        {
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$category" },
                { "subcategories", new BsonDocument("$addToSet", "$subcategory") }
            }),
            new BsonDocument("$project", new BsonDocument
            {
                { "_id", 0 },
                { "category", "$_id" },
                { "subcategories", new BsonDocument("$sortArray", new BsonDocument
                    {
                        { "input", "$subcategories" },
                        { "sortBy", 1 }
                    })
                }
            }),
            new BsonDocument("$sort", new BsonDocument("category", 1))
        };
}
