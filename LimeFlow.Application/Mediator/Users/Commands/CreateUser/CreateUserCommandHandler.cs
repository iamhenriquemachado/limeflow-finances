using BCrypt.Net;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Utils;
using LimeFlow.Domain.Models;
using MediatR;

namespace LimeFlow.Application.Mediator.Users.Commands.CreateUser
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

        public static Dictionary<string, string[]> ValidateErrors(CreateUserCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(request.Name))errors["name"] = ["Name is required."];
            if (request.Name?.Length > 100) errors["name"] = ["Cannot exceed 100 characters for the name"];
            if (string.IsNullOrWhiteSpace(request.Password)) errors["password"] = ["Password cannot be null or empty"];
            if (Functions.IsValidEmailAddress(request.Email) == false) errors["email"] = ["Invalid email address"];
            if (string.IsNullOrEmpty(request.Email)) errors["email"] = ["Email is empty"];


            if (errors.Count > 0)
            {
                return errors;
            }
            else
            {
                return null;
            }
        }
    }
}
