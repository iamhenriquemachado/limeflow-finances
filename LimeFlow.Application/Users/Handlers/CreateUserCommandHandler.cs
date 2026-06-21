using LimeFlow.Application.DTOs;
using LimeFlow.Application.Users.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Users.Handlers
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand rquest, CancellationToken cancellationToken)
        {
            var user = new UserDto();
        }
    }
}
