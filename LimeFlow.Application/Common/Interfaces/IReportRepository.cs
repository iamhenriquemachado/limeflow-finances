using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.Interfaces;

public interface IReportRepository
{
    Task<ReportSummaryResponse> GetSummaryAsync(Guid userId, DateTime? from, DateTime? to, CancellationToken ct = default);
    Task<IReadOnlyList<CategoryReportItem>> GetByCategoryAsync(
        Guid userId,
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        CancellationToken ct = default);
    Task<IReadOnlyList<MonthlyReportItem>> GetMonthlyAsync(Guid userId, int year, CancellationToken ct = default);
}
