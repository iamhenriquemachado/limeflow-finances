using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Interfaces;

public interface IGoalRepository
{
    Task<Goal?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<Goal>> GetAllAsync(Guid userId, CancellationToken ct = default);
    void Add(Goal goal);
    void Remove(Goal goal);
}
