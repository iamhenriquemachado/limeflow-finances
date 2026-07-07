using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;

namespace LimeFlow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _pass;


        public UserService(IUserRepository repo, IPasswordHasher pass)
        {
            _repo = repo;
            _pass = pass;
        }

        public async Task<IReadOnlyList<UserResponseDto>> GetUsersService()
        {
            var users = await _repo.GetAllAsync();
            var userResponseDto = users.Select(u => new UserResponseDto(u.Id, u.Name, u.Email, u.CreatedAt)).ToList();

            return userResponseDto;
        }

        public async Task<UserResponseDto> GetUserByIdService(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            var userResponseDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

            return userResponseDto;
        }

        public async Task<UserResponseDto> CreateUserService(CreateUserRequestDto request)
        {

            var encryptedPassword = _pass.PasswordHasher(request.password);

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.name,
                Email = request.email,
                Password = encryptedPassword,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow

            };

            await _repo.CreateAsync(user);

            var userResponseDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

            return userResponseDto;

        }

        public async Task DeleteUserService(Guid id)
        {
            await _repo.DeleteAsync(id);

        }
    }
}
