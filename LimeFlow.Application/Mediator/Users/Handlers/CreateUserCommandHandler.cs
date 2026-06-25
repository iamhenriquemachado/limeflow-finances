using BCrypt.Net;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Utils;
using LimeFlow.Application.Mediator.Users.Commands;
using LimeFlow.Domain.Models;
using MediatR;

namespace LimeFlow.Application.Mediator.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _repo;

        public CreateUserCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, 12);

            
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.CreateAsync(user);

            var userResponseDto = new UserResponseDto(user.Id, user.Email, user.Name, user.CreatedAt);

            return userResponseDto;
        }

    }
}
