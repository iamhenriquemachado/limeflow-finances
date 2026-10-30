using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 100);
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Password).StrongPassword();
    }
}
