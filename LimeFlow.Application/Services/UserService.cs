using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<UserResponseDto>> GetUsersService()
        {
            var users = await _repo.GetAllAsync();
            var userResponseDto = users.Select(u => new UserResponseDto(u.Id, u.Name, u.Email, u.CreatedAt));

            return (IReadOnlyList<UserResponseDto>)userResponseDto;
        }

        public async Task<UserResponseDto> GetUserByIdService(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            var userResponseDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

            return userResponseDto;
        }

        public async Task<UserResponseDto> CreateUserService(CreateUserRequestDto request)
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.name,
                Email = request.email,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow

            };

            await _repo.CreateAsync(user);

            var userResponseDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

            return userResponseDto;

        }
    }
}
