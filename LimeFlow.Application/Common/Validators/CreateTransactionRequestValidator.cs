using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(t => t.AccountId).NotEmpty();
        RuleFor(t => t.CategoryId).NotEmpty();
        RuleFor(t => t.Type).IsInEnum();
        RuleFor(t => t.Amount).GreaterThan(0).PrecisionScale(2, 18, false);
        RuleFor(t => t.Description).NotEmpty().MaximumLength(200);
        RuleFor(t => t.TransactionDate)
            .Must(date => date <= DateTime.UtcNow)
            .When(t => t.TransactionDate.HasValue)
            .WithMessage("Transaction date cannot be in the future.");
    }
}
