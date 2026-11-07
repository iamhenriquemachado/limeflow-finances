using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class GoalWithdrawalRequestValidator : AbstractValidator<GoalWithdrawalRequest>
{
    public GoalWithdrawalRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).PrecisionScale(2, 18, false);
    }
}
