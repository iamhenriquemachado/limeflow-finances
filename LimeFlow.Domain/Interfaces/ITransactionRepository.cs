using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task CreateTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Guid id);
        Task DeleteTransactionAsync(int id);
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<IReadOnlyList<Transaction>> GetAllTransactionAsync();
    }
}
