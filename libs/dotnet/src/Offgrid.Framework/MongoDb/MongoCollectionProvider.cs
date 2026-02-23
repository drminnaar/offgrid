using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Offgrid.Framework.MongoDb;

public sealed class MongoCollectionProvider : IMongoCollectionProvider
{
    private readonly ILogger<MongoCollectionProvider> _logger;
    private readonly string _databaseName;
    private readonly IMongoDatabase _database;

    public MongoCollectionProvider(ILogger<MongoCollectionProvider> logger, MongoDatabaseOptions options)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(options);
        _databaseName = options.DatabaseName;
        _logger = logger;
        _database = GetDatabase(options);
    }

    private static IMongoDatabase GetDatabase(MongoDatabaseOptions databaseOptions)
    {
        var client = new MongoClient(databaseOptions.ConnectionString);
        return client.GetDatabase(databaseOptions.DatabaseName);
    }

    public IMongoCollection<TMongoEntity> GetCollection<TMongoEntity>(string collectionName) where TMongoEntity : class, IMongoEntity
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName);
        try
        {
            return _database.GetCollection<TMongoEntity>(collectionName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting collection {CollectionName} from database {DatabaseName}", collectionName, _databaseName);
            throw;
        }
    }
}
