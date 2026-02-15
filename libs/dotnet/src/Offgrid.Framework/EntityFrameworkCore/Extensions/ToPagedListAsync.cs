using Microsoft.EntityFrameworkCore;
using Offgrid.Framework.System.Collections.Generic;

namespace Offgrid.Framework.EntityFrameworkCore.Extensions;

public static partial class EntityFrameworkCoreExtensions
{
    /// <summary>
    /// Converts an <see cref="IQueryable{T}"/> to a paged list asynchronously.
    /// </summary>
    /// <param name="source">The source queryable.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="token">The cancellation token.</param>
    /// <typeparam name="T">The type of the elements in the source queryable.</typeparam>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged list.</returns>
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken token)
    {
        var itemCount = await source.LongCountAsync(token);

        var items = await source
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(token);

        return new PagedList<T>(items, itemCount, pageNumber, pageSize);
    }
}
