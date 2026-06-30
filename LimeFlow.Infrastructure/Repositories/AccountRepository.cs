using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;
using LimeFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Accounts.Where(a => a.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IReadOnlyList<Account>> GetAllAsync(Guid userId)
        {
            var accounts = await _context.Accounts.Where(u => u.Id == userId).ToListAsync();

            return accounts;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var account = await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            return account;
        }

        public async Task UpdateAsync(Account account)
        {
            await _context.SaveChangesAsync();

        }
    }
}
