using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User> GetByIdAsync(Guid id);
        Task<IReadOnlyList<User>> GetAllAsync();

    }
}
