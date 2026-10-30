using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        // TODO(Phase 4): Name is required, 1-50 characters.
        // TODO(Phase 4): Type must be a defined TransactionType value (IsInEnum).
    }
}
