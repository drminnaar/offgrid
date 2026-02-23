using MongoDB.Driver;

namespace Offgrid.Framework.MongoDb;

public interface IMongoCollectionProvider
{
    IMongoCollection<TMongoEntity> GetCollection<TMongoEntity>(string collectionName) where TMongoEntity : class, IMongoEntity;
}
