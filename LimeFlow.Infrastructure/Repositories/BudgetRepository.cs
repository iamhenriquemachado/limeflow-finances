using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly AppDbContext _context;

    public BudgetRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Budget?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default) =>
        _context.Budgets.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId, ct);

    public async Task<IReadOnlyList<Budget>> GetAllAsync(Guid userId, CancellationToken ct = default) =>
        await _context.Budgets
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.CreatedAt)
            .ToListAsync(ct);

    public Task<bool> ExistsForCategoryAndPeriodAsync(
        Guid userId,
        Guid categoryId,
        BudgetPeriod period,
        Guid? excludeId = null,
        CancellationToken ct = default) =>
        _context.Budgets.AnyAsync(b =>
            b.UserId == userId
            && b.CategoryId == categoryId
            && b.Period == period
            && (excludeId == null || b.Id != excludeId), ct);

    public async Task<decimal> SumExpensesAsync(
        Guid userId,
        Guid categoryId,
        DateTime periodStart,
        DateTime periodEnd,
        CancellationToken ct = default)
    {
        var total = await _context.Transactions
            .AsNoTracking()
            .Where(t => t.CategoryId == categoryId
                && t.Type == TransactionType.Expense
                && t.TransactionDate >= periodStart
                && t.TransactionDate <= periodEnd
                && _context.Accounts.Any(a => a.Id == t.AccountId && a.UserId == userId))
            .SumAsync(t => (decimal?)t.Amount, ct);

        return total ?? 0m;
    }

    public void Add(Budget budget) => _context.Budgets.Add(budget);

    public void Remove(Budget budget) => _context.Budgets.Remove(budget);
}
