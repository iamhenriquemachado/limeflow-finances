using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<Category>> GetAllAsync(Guid userId, TransactionType? type, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(Guid userId, string name, Guid? excludeId = null, CancellationToken ct = default);
    Task<bool> IsInUseAsync(Guid id, CancellationToken ct = default);
    void Add(Category category);
    void Remove(Category category);
}
