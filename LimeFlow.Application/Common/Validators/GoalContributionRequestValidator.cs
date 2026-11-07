using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class GoalContributionRequestValidator : AbstractValidator<GoalContributionRequest>
{
    public GoalContributionRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).PrecisionScale(2, 18, false);
        RuleFor(x => x.FromAccountId)
            .NotEmpty()
            .When(x => x.FromAccountId.HasValue);
    }
}
