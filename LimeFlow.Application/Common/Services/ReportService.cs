using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly ICurrentUserService _currentUser;

    public ReportService(IReportRepository reportRepository, ICurrentUserService currentUser)
    {
        _reportRepository = reportRepository;
        _currentUser = currentUser;
    }

    public Task<ReportSummaryResponse> GetSummaryAsync(DateTime? from, DateTime? to, CancellationToken ct = default)
    {
        // TODO(Phase 8): delegate to _reportRepository.GetSummaryAsync(_currentUser.UserId, from, to, ct).
        // totalBalance is the sum of the user's account balances; income/expense/net come from transactions in the range.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<CategoryReportItem>> GetByCategoryAsync(
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        CancellationToken ct = default)
    {
        // TODO(Phase 8): delegate to _reportRepository.GetByCategoryAsync(...). Percent is total / grandTotal * 100.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<MonthlyReportItem>> GetMonthlyAsync(int year, CancellationToken ct = default)
    {
        // TODO(Phase 8): delegate to _reportRepository.GetMonthlyAsync(...). Always return 12 months (zero-fill missing ones).
        throw new NotImplementedException();
    }
}
