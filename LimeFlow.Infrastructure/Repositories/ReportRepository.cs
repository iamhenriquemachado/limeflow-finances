using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;

namespace LimeFlow.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<ReportSummaryResponse> GetSummaryAsync(Guid userId, DateTime? from, DateTime? to, CancellationToken ct = default)
    {
        // TODO(Phase 8): AsNoTracking projections. totalBalance = sum of Accounts.Balance for userId.
        // totalIncome / totalExpense = sum of Transactions (join Accounts) in the date range, grouped by Type.
        // net = income - expense. Never load full entities into memory.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<CategoryReportItem>> GetByCategoryAsync(
        Guid userId,
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        CancellationToken ct = default)
    {
        // TODO(Phase 8): GroupBy CategoryId in SQL, join Categories for the name, compute percent against the grand total.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<MonthlyReportItem>> GetMonthlyAsync(Guid userId, int year, CancellationToken ct = default)
    {
        // TODO(Phase 8): GroupBy month of TransactionDate for the given year. Return 12 items, zero-filling months with no data.
        throw new NotImplementedException();
    }
}
