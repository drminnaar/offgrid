using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoInit.Configuration;

namespace MongoInit.Data;

public sealed class ProductCollection
{
    public const string CollectionName = "products";

    private readonly string _databaseName;
    private readonly string _connectionString;
    private readonly ILogger<ProductCollection> _logger;

    public ProductCollection(ILogger<ProductCollection> logger, IOptions<DatabaseOptions> options)
    {
        _databaseName = options.Value.DatabaseName;
        _connectionString = options.Value.ConnectionString;
        _logger = logger;
    }

    public async Task SaveProductsAsync(List<Product> products)
    {
        var client = new MongoClient(_connectionString);
        var database = client.GetDatabase(_databaseName);
        var collection = database.GetCollection<Product>(CollectionName);
        await collection.DeleteManyAsync(FilterDefinition<Product>.Empty);
        await collection.InsertManyAsync(products);
        _logger.LogInformation("💾 Inserted {ProductCount} products into the '{CollectionName}' collection in database '{DatabaseName}'.",
            products.Count, CollectionName, _databaseName);
    }
}
