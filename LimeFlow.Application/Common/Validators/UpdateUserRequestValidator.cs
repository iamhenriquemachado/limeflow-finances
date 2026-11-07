using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        // TODO(Phase 3): Name is required, 3-100 characters (same rule as CreateUserRequestValidator).
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(100);
    }
}
