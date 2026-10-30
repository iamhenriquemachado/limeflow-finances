using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateGoalRequestValidator : AbstractValidator<CreateGoalRequest>
{
    public CreateGoalRequestValidator()
    {
        // TODO(Phase 7): Name is required, max 100 characters.
        // TODO(Phase 7): Description is required, max 500 characters.
        // TODO(Phase 7): Target must be greater than 0, with at most 2 decimal places.
        // TODO(Phase 7): TargetDate must be in the future.
        // TODO(Phase 7): MonthlyContribution must be greater than or equal to 0.
        // TODO(Phase 7): Icon is required, max 50 characters.
    }
}
