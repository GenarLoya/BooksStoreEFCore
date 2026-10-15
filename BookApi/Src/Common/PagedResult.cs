namespace BookApi.Src.Common;

public record PagedResult<T>(
    IEnumerable<T> Items,
    int Page,
    int PageSize,
    int TotalItems)
{
    public int TotalPages => PageSize == 0
        ? 0
        : (int)Math.Ceiling(TotalItems / (double)PageSize);

    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;
}
