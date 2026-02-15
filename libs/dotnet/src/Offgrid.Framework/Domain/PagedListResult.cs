namespace Offgrid.Framework.Domain;

public sealed record PagedListResult<T>
{
    public T[] Items { get; init; } = [];
    public int CurrentPageNumber { get; init; }
    public long ItemCount { get; init; }
    public int PageSize { get; init; }
    public int PageCount { get; init; }
    public int LastPageNumber { get; init; }
    public int? NextPageNumber { get; init; }
    public int? PreviousPageNumber { get; init; }
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }
}
