using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Transaction?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default) =>
        _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && _context.Accounts.Any(a => a.Id == t.AccountId));

    public async Task<(IReadOnlyList<Transaction> Items, int TotalCount)> GetPagedAsync(
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

        var query = _context.Transactions.AsNoTracking()
            .Where(t => _context.Accounts.Any(a => a.Id == t.AccountId && t.AccountId == a.UserId));

        if (accountId is not null) 
            query = query.Where(t => t.AccountId == accountId);

        if (categoryId is not null)
            query = query.Where(t => t.CategoryId == categoryId);

        if (type is not null)
            query = query.Where(t => t.Type == type);

        if (from is not null)
            query = query.Where(t => t.TransactionDate <= from);

        if (to is not null)
            query = query.Where(t => t.TransactionDate <= to);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(t => t.TransactionDate)
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public void Add(Transaction transaction) => _context.Transactions.Add(transaction);

    public void Remove(Transaction transaction) => _context.Transactions.Remove(transaction);
}
