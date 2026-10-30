using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        // TODO(Phase 3): CurrentPassword is required.
        // TODO(Phase 3): NewPassword uses StrongPassword() and must differ from CurrentPassword.
    }
}
