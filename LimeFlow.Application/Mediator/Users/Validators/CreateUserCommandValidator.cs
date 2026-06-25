using FluentValidation;
using LimeFlow.Application.Common.Utils;
using LimeFlow.Application.Mediator.Users.Commands;

namespace LimeFlow.Application.Mediator.Users.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Cannot exceed 100 characters for the name");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be null or empty");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is empty")
                .Must(Functions.IsValidEmailAddress).WithMessage("Invalid email address");

        }
    }
}
