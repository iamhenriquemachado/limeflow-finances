using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.Interfaces;

public interface IBudgetRepository
{
    Task<Budget?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<Budget>> GetAllAsync(Guid userId, CancellationToken ct = default);
    Task<bool> ExistsForCategoryAndPeriodAsync(
        Guid userId,
        Guid categoryId,
        BudgetPeriod period,
        Guid? excludeId = null,
        CancellationToken ct = default);
    void Add(Budget budget);
    void Remove(Budget budget);
}
