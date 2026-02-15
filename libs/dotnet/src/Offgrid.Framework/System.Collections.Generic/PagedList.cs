using System.Collections;
using System.Collections.ObjectModel;

namespace Offgrid.Framework.System.Collections.Generic;

public sealed class PagedList<T> : IPagedList<T>
{
    private readonly ReadOnlyCollection<T> _items;

    public PagedList(List<T> items, long itemCount, int pageNumber, int pageSize)
    {
        _items = new List<T>(items).AsReadOnly();
        ItemCount = itemCount;
        CurrentPageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = ComputePageCount(pageSize, itemCount);
    }

    public int CurrentPageNumber { get; private init; }
    public long ItemCount { get; private init; }
    public int PageSize { get; private init; }
    public int PageCount { get; private init; }
    public int LastPageNumber => PageCount;
    public int? NextPageNumber => HasNext ? CurrentPageNumber + 1 : default(int?);
    public int? PreviousPageNumber => HasPrevious ? CurrentPageNumber - 1 : default(int?);
    public bool HasPrevious => CurrentPageNumber > 1;
    public bool HasNext => CurrentPageNumber < PageCount;

    private static int ComputePageCount(int pageSize, long itemCount) => pageSize > 0
        ? (int)Math.Ceiling(itemCount / (double)pageSize)
        : 0;

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
}
