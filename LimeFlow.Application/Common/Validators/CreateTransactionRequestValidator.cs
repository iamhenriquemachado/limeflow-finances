using FluentValidation;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Validators;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Amount).GreaterThan(0).PrecisionScale(18, 2, false);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        RuleFor(x => x.TransactionDate)
            .Must(date => date <= DateTime.UtcNow)
            .When(x => x.TransactionDate.HasValue)
            .WithMessage("Transaction date cannot be in the future.");
    }
}
