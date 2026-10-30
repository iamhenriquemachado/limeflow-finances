using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class GoalWithdrawalRequestValidator : AbstractValidator<GoalWithdrawalRequest>
{
    public GoalWithdrawalRequestValidator()
    {
        // TODO(Phase 7): Amount must be greater than 0, with at most 2 decimal places.
    }
}
