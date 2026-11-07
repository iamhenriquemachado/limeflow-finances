using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReportSummaryResponse> GetSummaryAsync(
        Guid userId,
        DateTime? from,
        DateTime? to,
        CancellationToken ct = default)
    {
        var totalBalance = await _context.Accounts
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .SumAsync(a => (decimal?)a.Balance, ct) ?? 0m;

        var totals = await OwnedTransactions(userId, from, to)
            .GroupBy(t => t.Type)
            .Select(g => new { Type = g.Key, Total = g.Sum(t => t.Amount) })
            .ToListAsync(ct);

        var totalIncome = totals.FirstOrDefault(t => t.Type == TransactionType.Income)?.Total ?? 0m;
        var totalExpense = totals.FirstOrDefault(t => t.Type == TransactionType.Expense)?.Total ?? 0m;

        return new ReportSummaryResponse(totalBalance, totalIncome, totalExpense, totalIncome - totalExpense);
    }

    public async Task<IReadOnlyList<CategoryReportItem>> GetByCategoryAsync(
        Guid userId,
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        CancellationToken ct = default)
    {
        var transactions = OwnedTransactions(userId, from, to);
        if (type is not null)
            transactions = transactions.Where(t => t.Type == type);

        var rows = await (
            from t in transactions
            join c in _context.Categories.AsNoTracking() on t.CategoryId equals c.Id
            group t by new { t.CategoryId, c.Name } into g
            select new
            {
                g.Key.CategoryId,
                g.Key.Name,
                Total = g.Sum(t => t.Amount)
            }).ToListAsync(ct);

        var grandTotal = rows.Sum(r => r.Total);

        return rows
            .OrderBy(r => r.Name)
            .Select(r => new CategoryReportItem(
                r.CategoryId,
                r.Name,
                r.Total,
                grandTotal == 0 ? 0 : Math.Round(r.Total / grandTotal * 100m, 2)))
            .ToList();
    }

    public async Task<IReadOnlyList<MonthlyReportItem>> GetMonthlyAsync(
        Guid userId,
        int year,
        CancellationToken ct = default)
    {
        var start = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = start.AddYears(1);

        var rows = await OwnedTransactions(userId, start, end.AddTicks(-1))
            .GroupBy(t => t.TransactionDate.Month)
            .Select(g => new
            {
                Month = g.Key,
                Income = g.Sum(t => t.Type == TransactionType.Income ? t.Amount : 0m),
                Expense = g.Sum(t => t.Type == TransactionType.Expense ? t.Amount : 0m)
            })
            .ToListAsync(ct);

        var byMonth = rows.ToDictionary(r => r.Month);

        return Enumerable.Range(1, 12)
            .Select(month => byMonth.TryGetValue(month, out var row)
                ? new MonthlyReportItem(month, row.Income, row.Expense)
                : new MonthlyReportItem(month, 0m, 0m))
            .ToList();
    }

    private IQueryable<Transaction> OwnedTransactions(Guid userId, DateTime? from, DateTime? to)
    {
        var query = _context.Transactions
            .AsNoTracking()
            .Where(t => _context.Accounts.Any(a => a.Id == t.AccountId && a.UserId == userId));

        if (from is not null)
            query = query.Where(t => t.TransactionDate >= from);

        if (to is not null)
            query = query.Where(t => t.TransactionDate <= to);

        return query;
    }
}
