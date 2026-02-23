using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Offgrid.Framework.System.Collections.Generic;

namespace Offgrid.Framework.MongoDb;

public interface IMongoRepository<TMongoEntity> where TMongoEntity : class, IMongoEntity
{
    Task AddAsync(
        TMongoEntity entity,
        CancellationToken cancellationToken = default);

    Task AddManyAsync(
        IEnumerable<TMongoEntity> entities,
        CancellationToken cancellationToken = default);

    Task DeleteByIdAsync(
        ObjectId id,
        CancellationToken cancellationToken = default);

    Task<long> DeleteManyAsync(
        Expression<Func<TMongoEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<PagedList<TMongoEntity>> FindAsync(
        IMongoQuery query,
        FilterDefinition<TMongoEntity> filter,
        SortDefinition<TMongoEntity> sort,
        CancellationToken ct = default);

    Task<TMongoEntity?> FindByIdAsync(
        ObjectId id,
        CancellationToken cancellationToken = default);

    Task<bool> ReplaceAsync(
        ObjectId id,
        TMongoEntity entity,
        CancellationToken cancellationToken = default);
}
