using LimeFlow.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Mediator.Users.Commands
{
    public record CreateUserCommand(string Name, string Email, string Password) : IRequest<UserResponseDto>;
}
