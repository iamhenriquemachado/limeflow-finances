using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;

namespace LimeFlow.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly AppDbContext _context;

    public BudgetRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Budget?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default)
    {
        // TODO(Phase 6): FirstOrDefaultAsync where Id == id && UserId == userId.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Budget>> GetAllAsync(Guid userId, CancellationToken ct = default)
    {
        // TODO(Phase 6): AsNoTracking, filter by userId, OrderBy CreatedAt.
        throw new NotImplementedException();
    }

    public Task<bool> ExistsForCategoryAndPeriodAsync(
        Guid userId,
        Guid categoryId,
        BudgetPeriod period,
        Guid? excludeId = null,
        CancellationToken ct = default)
    {
        // TODO(Phase 6): AnyAsync on UserId + CategoryId + Period, excluding excludeId when set.
        throw new NotImplementedException();
    }

    public void Add(Budget budget) => _context.Budgets.Add(budget);

    public void Remove(Budget budget) => _context.Budgets.Remove(budget);
}
