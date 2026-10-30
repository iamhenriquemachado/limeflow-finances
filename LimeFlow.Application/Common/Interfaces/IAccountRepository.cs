using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<Account>> GetAllAsync(Guid userId, CancellationToken ct = default);
    void Add(Account account);
    void Remove(Account account);
}
