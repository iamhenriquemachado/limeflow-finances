using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Services
{
    public class UserService
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
    }
}
