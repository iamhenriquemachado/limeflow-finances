using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class UpdateBudgetRequestValidator : AbstractValidator<UpdateBudgetRequest>
{
    public UpdateBudgetRequestValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.LimitAmount).GreaterThan(0).PrecisionScale(18, 2, false);
        RuleFor(x => x.AlertThreshold).InclusiveBetween(1, 100);
        RuleFor(x => x.Period).IsInEnum();
        RuleFor(x => x.Notes).MaximumLength(500);
    }
}
