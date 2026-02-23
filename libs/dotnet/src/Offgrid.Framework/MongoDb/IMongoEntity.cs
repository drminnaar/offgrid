using MongoDB.Bson;

namespace Offgrid.Framework.MongoDb;

public interface IMongoEntity
{
    public ObjectId Id { get; set; }
}
