using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(int id);
        Task DeleteAccountAsync(int id);
        Task<Account> GetAccountAsync(Guid id);
        Task<IReadOnlyList<Account>> GetAllAccountAsync();
    }
}
