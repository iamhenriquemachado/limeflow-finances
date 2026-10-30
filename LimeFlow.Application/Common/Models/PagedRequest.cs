namespace LimeFlow.Application.Common.Models;

public record PagedRequest
{
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    public int? Page { get; init; }
    public int? PageSize { get; init; }

    public int PageNumber => Math.Max(Page ?? 1, 1);
    public int Take => Math.Clamp(PageSize ?? DefaultPageSize, 1, MaxPageSize);
    public int Skip => (PageNumber - 1) * Take;
}
