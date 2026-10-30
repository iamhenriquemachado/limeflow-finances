using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateBudgetRequestValidator : AbstractValidator<CreateBudgetRequest>
{
    public CreateBudgetRequestValidator()
    {
        // TODO(Phase 6): CategoryId is required (not empty Guid).
        // TODO(Phase 6): LimitAmount must be greater than 0, with at most 2 decimal places.
        // TODO(Phase 6): AlertThreshold is an integer between 1 and 100 (percent).
        // TODO(Phase 6): Period must be a defined BudgetPeriod value (IsInEnum).
        // TODO(Phase 6): Notes is optional, max 500 characters.
    }
}
