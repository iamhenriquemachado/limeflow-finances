using FluentValidation;
using LimeFlow.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Handler
{
    internal class CreateUserCommandHandler
    {
        private readonly IValidator<CreateUserCommand> _validator;

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);
        }
    }
}
