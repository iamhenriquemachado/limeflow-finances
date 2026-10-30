using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Models;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<UserDataResponseDto> GetByEmailAsync(string email);
        Task CreateAsync(User user);
        Task UpdateAsync(Guid id, User user);
        Task DeleteAsync(Guid id);


    }
}
