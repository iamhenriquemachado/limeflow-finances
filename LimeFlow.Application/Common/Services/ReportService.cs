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

    public Task<ReportSummaryResponse> GetSummaryAsync(DateTime? from, DateTime? to, CancellationToken ct = default) =>
        _reportRepository.GetSummaryAsync(_currentUser.UserId, from, to, ct);

    public Task<IReadOnlyList<CategoryReportItem>> GetByCategoryAsync(
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        CancellationToken ct = default) =>
        _reportRepository.GetByCategoryAsync(_currentUser.UserId, from, to, type, ct);

    public Task<IReadOnlyList<MonthlyReportItem>> GetMonthlyAsync(int year, CancellationToken ct = default) =>
        _reportRepository.GetMonthlyAsync(_currentUser.UserId, year, ct);
}
