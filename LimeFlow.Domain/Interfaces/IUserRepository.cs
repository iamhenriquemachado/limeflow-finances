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
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(int id);
        Task DeletesUserAsync(int id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<IReadOnlyList<User>> GetAllUsersAsync();

    }
}
