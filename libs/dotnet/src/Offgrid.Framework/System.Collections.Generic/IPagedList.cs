namespace Offgrid.Framework.System.Collections.Generic;

public interface IPagedList<T> : IEnumerable<T>
{
    int CurrentPageNumber { get; }
    int? NextPageNumber { get; }
    int? PreviousPageNumber { get; }
    int LastPageNumber { get; }
    long ItemCount { get; }
    int PageSize { get; }
    int PageCount { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }
}
