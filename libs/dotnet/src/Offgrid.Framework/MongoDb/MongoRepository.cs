using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Offgrid.Framework.MongoDb.Extensions;
using Offgrid.Framework.System.Collections.Generic;

namespace Offgrid.Framework.MongoDb;

public class MongoRepository<TMongoEntity> : IMongoRepository<TMongoEntity> where TMongoEntity : class, IMongoEntity
{
    private readonly ILogger<MongoRepository<TMongoEntity>> _logger;
    private readonly IMongoCollection<TMongoEntity> _collection;
    private readonly string _collectionName;

    public MongoRepository(
        ILogger<MongoRepository<TMongoEntity>> logger,
        IMongoCollectionProvider collectionProvider,
        string collectionName)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(collectionProvider);
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName);

        _logger = logger;
        _collectionName = collectionName;
        _collection = collectionProvider.GetCollection<TMongoEntity>(collectionName);
    }

    public string CollectionName => _collectionName;
    protected IMongoCollection<TMongoEntity> Collection => _collection;

    public async Task AddAsync(TMongoEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding entity to collection {CollectionName}", _collectionName);
            throw;
        }
    }

    public async Task AddManyAsync(IEnumerable<TMongoEntity> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding entities to collection {CollectionName}", _collectionName);
            throw;
        }
    }

    public async Task DeleteByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        try
        {
            var filter = Builders<TMongoEntity>.Filter.Eq(e => e.Id, id);
            await _collection.DeleteOneAsync(filter, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting entity from collection {CollectionName}", _collectionName);
            throw;
        }
    }

    public async Task<long> DeleteManyAsync(
        Expression<Func<TMongoEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var filter = new ExpressionFilterDefinition<TMongoEntity>(predicate);
            return await _collection
                .DeleteManyAsync(filter, cancellationToken)
                .ContinueWith(t => t.Result.DeletedCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting entities from collection {CollectionName}", _collectionName);
            throw;
        }
    }

    public async Task<PagedList<TMongoEntity>> FindAsync(
        IMongoQuery query,
        FilterDefinition<TMongoEntity> filter,
        SortDefinition<TMongoEntity> sort,
        CancellationToken ct = default)
    {
        // Get total count first (for pagination metadata)
        var totalCount = await _collection.CountDocumentsAsync(filter, cancellationToken: ct);

        if (totalCount == 0)
        {
            return PagedList<TMongoEntity>.Empty;
        }

        var options = new FindOptions<TMongoEntity>
        {
            Sort = sort,
            Skip = (query.Page - 1) * query.PageSize,
            Limit = query.PageSize
        };

        using var cursor = await _collection.FindAsync(filter, options, ct);

        return await cursor.ToPagedListAsync(query.Page, query.PageSize, totalCount, ct);
    }

    public async Task<TMongoEntity?> FindByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        try
        {
            var filter = Builders<TMongoEntity>.Filter.Eq(e => e.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding entity by Id '{CollectionId}' in collection {CollectionName}", id, _collectionName);
            throw;
        }
    }

    public async Task<bool> ReplaceAsync(ObjectId id, TMongoEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var filter = Builders<TMongoEntity>.Filter.Eq(e => e.Id, id);
            var result = await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            return result.IsModifiedCountAvailable && result.ModifiedCount > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error replacing entity with Id '{CollectionId}' in collection {CollectionName}", id, _collectionName);
            throw;
        }
    }
}
