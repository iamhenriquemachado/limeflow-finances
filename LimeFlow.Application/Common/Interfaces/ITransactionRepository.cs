using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);
    Task<(IReadOnlyList<Transaction> Items, int TotalCount)> GetPagedAsync(
        Guid userId,
        Guid? accountId,
        Guid? categoryId,
        TransactionType? type,
        DateTime? from,
        DateTime? to,
        int skip,
        int take,
        CancellationToken ct = default);
    void Add(Transaction transaction);
    void Remove(Transaction transaction);
}
