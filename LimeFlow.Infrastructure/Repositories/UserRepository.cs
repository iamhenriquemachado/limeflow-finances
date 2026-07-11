using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;
using LimeFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(User user)
        {

            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var userFound = await _context.Users.Where(user => user.Id == id).ExecuteDeleteAsync();

        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }

        public async Task UpdateUserAsync(Guid id, User user)
        {
            var u = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (u != null)
            {
                u.Id = user.Id;
                u.Name = user.Name;
                u.Email = user.Email;
                u.Password = user.Password;

                await _context.SaveChangesAsync();
            }


        }

        public async Task<UserDataResponseDto> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            
            if (user != null)
            {
                var userResponseDto = new UserDataResponseDto(user.Id, user.Email, user.Password, user.Name, user.CreatedAt);

                return userResponseDto;
            }

            return null;
        }
    }
}
