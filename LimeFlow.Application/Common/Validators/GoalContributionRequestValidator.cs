using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class GoalContributionRequestValidator : AbstractValidator<GoalContributionRequest>
{
    public GoalContributionRequestValidator()
    {
        // TODO(Phase 7): Amount must be greater than 0, with at most 2 decimal places.
        // TODO(Phase 7): FromAccountId, when provided, must not be an empty Guid.
    }
}
