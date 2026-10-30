using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class UpdateGoalRequestValidator : AbstractValidator<UpdateGoalRequest>
{
    public UpdateGoalRequestValidator()
    {
        // TODO(Phase 7): same rules as CreateGoalRequestValidator.
    }
}
