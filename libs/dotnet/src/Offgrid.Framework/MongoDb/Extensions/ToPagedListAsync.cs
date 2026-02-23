using MongoDB.Driver;
using Offgrid.Framework.System.Collections.Generic;

namespace Offgrid.Framework.MongoDb.Extensions;

public static partial class MongoExtensions
{
    public static async Task<PagedList<TMongoEntity>> ToPagedListAsync<TMongoEntity>(
        this IAsyncCursor<TMongoEntity>? cursor,
        int pageNumber,
        int pageSize,
        long totalCount,
        CancellationToken ct = default) where TMongoEntity : class, IMongoEntity
    {
        if (cursor == null)
        {
            return PagedList<TMongoEntity>.Empty;
        }

        var items = new List<TMongoEntity>();
        while (await cursor.MoveNextAsync(ct))
        {
            items.AddRange(cursor.Current);
        }

        return new PagedList<TMongoEntity>(items, totalCount, pageNumber, pageSize);
    }
}
