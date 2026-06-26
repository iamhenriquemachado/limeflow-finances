using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Mediator.Users.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Mediator.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repo;
        public DeleteUserCommand(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteUserCommand request, Guid id)
        {

        }
    }
}
