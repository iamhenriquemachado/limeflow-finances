using FluentValidation;
using System.Text.RegularExpressions;

namespace LimeFlow.Application.Common.Validators;

public static partial class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty()
            .MaximumLength(254)
            .Must(email => EmailRegex().IsMatch(email ?? string.Empty))
            .WithMessage("Invalid email address.");

    public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.");

    [GeneratedRegex("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();
}
