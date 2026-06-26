using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Mediator.Users.Commands
{
    public record DeleteUserCommand(Guid id);
}
