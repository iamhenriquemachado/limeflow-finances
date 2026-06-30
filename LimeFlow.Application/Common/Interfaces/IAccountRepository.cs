using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Guid id);
        Task<Account> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Account>> GetAllAsync(Guid userId);
    }
}
