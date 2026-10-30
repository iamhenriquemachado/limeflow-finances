using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Account?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default) =>
        _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId, ct);

    public async Task<IReadOnlyList<Account>> GetAllAsync(Guid userId, CancellationToken ct = default) =>
        await _context.Accounts
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.Name)
            .ToListAsync(ct);

    public void Add(Account account) => _context.Accounts.Add(account);

    public void Remove(Account account) => _context.Accounts.Remove(account);
}
