using FluentValidation;
using LimeFlow.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Validator
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("The user identifier can't be empty.");

            RuleFor(command => command.Name)
                .NotNull()
                .WithMessage("The user name can't be null.");

            RuleFor(command => command.Email)
                .NotNull()
                .WithMessage("The user email can't be null.");

            RuleFor(command => command.Password)
            .NotNull()
            .WithMessage("The user password can't be null.");
        }
    }
}
