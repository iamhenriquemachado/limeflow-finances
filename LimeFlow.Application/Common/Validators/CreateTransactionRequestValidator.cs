using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        // TODO(Phase 5): AccountId and CategoryId are required (not empty Guid).
        // TODO(Phase 5): Type must be a defined TransactionType value (IsInEnum).
        // TODO(Phase 5): Amount must be greater than 0, with at most 2 decimal places.
        // TODO(Phase 5): Description is required, max 200 characters.
        // TODO(Phase 5): TransactionDate, when provided, cannot be in the future.
    }
}
