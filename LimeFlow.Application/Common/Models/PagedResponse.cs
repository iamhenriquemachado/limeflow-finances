namespace LimeFlow.Application.Common.Models;

public record PagedResponse<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount);
