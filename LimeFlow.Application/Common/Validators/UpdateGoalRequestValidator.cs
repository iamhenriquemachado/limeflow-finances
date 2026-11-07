using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class UpdateGoalRequestValidator : AbstractValidator<UpdateGoalRequest>
{
    public UpdateGoalRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Target).GreaterThan(0).PrecisionScale(18, 2, false);
        RuleFor(x => x.TargetDate)
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Target date must be in the future.");
        RuleFor(x => x.MonthlyContribution).GreaterThanOrEqualTo(0).PrecisionScale(18, 2, false);
        RuleFor(x => x.Icon).NotEmpty().MaximumLength(50);
    }
}
