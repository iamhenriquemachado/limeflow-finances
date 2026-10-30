using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.Interfaces;

public interface IReportService
{
    Task<ReportSummaryResponse> GetSummaryAsync(DateTime? from, DateTime? to, CancellationToken ct = default);
    Task<IReadOnlyList<CategoryReportItem>> GetByCategoryAsync(
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        CancellationToken ct = default);
    Task<IReadOnlyList<MonthlyReportItem>> GetMonthlyAsync(int year, CancellationToken ct = default);
}
