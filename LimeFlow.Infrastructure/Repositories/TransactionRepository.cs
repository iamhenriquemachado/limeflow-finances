using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;

namespace LimeFlow.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Transaction?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default)
    {
        // TODO(Phase 5): join Accounts and return the transaction only when Account.UserId == userId.
        throw new NotImplementedException();
    }

    public Task<(IReadOnlyList<Transaction> Items, int TotalCount)> GetPagedAsync(
        Guid userId,
        Guid? accountId,
        Guid? categoryId,
        TransactionType? type,
        DateTime? from,
        DateTime? to,
        int skip,
        int take,
        CancellationToken ct = default)
    {
        // TODO(Phase 5):
        // 1. AsNoTracking query joining Accounts, filter Account.UserId == userId.
        // 2. Optional filters: accountId, categoryId, type, TransactionDate >= from, TransactionDate <= to.
        // 3. CountAsync for totalCount; then OrderByDescending TransactionDate, Skip/Take.
        throw new NotImplementedException();
    }

    public void Add(Transaction transaction) => _context.Transactions.Add(transaction);

    public void Remove(Transaction transaction) => _context.Transactions.Remove(transaction);
}
