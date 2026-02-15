using Offgrid.Framework.System.Collections.Generic;

namespace Offgrid.Framework.Domain.Extensions;

public static partial class DomainExtensions
{
    public static PagedListResult<TResult> ToPagedListResult<T, TResult>(this IPagedList<T> pagedList, Func<T, TResult> mapFunc)
    {
        ArgumentNullException.ThrowIfNull(pagedList, nameof(pagedList));
        ArgumentNullException.ThrowIfNull(mapFunc, nameof(mapFunc));
        return new PagedListResult<TResult>
        {
            Items = pagedList.Select(mapFunc).ToArray(),
            CurrentPageNumber = pagedList.CurrentPageNumber,
            HasNext = pagedList.HasNext,
            HasPrevious = pagedList.HasPrevious,
            ItemCount = pagedList.ItemCount,
            PageCount = pagedList.PageCount,
            PageSize = pagedList.PageSize,
            LastPageNumber = pagedList.LastPageNumber,
            NextPageNumber = pagedList.NextPageNumber,
            PreviousPageNumber = pagedList.PreviousPageNumber,
        };
    }
}
