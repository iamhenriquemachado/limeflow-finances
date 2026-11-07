using LimeFlow.Domain.Exceptions;

namespace LimeFlow.Domain.Entities;

public class Goal
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Target { get; private set; }
    public decimal AlreadySaved { get; private set; }
    public DateTime TargetDate { get; private set; }
    public decimal MonthlyContribution { get; private set; }
    public string Icon { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public decimal ProgressPercent => Target <= 0 ? 0 : Math.Min(100m, Math.Round(AlreadySaved / Target * 100m, 2));
    public bool IsCompleted => AlreadySaved >= Target;

    private Goal() { }

    public Goal(
        Guid userId,
        string name,
        string description,
        decimal target,
        DateTime targetDate,
        decimal monthlyContribution,
        string icon)
    {
        EnsureTarget(target);
        EnsureTargetDate(targetDate);
        EnsureMonthlyContribution(monthlyContribution);

        Id = Guid.NewGuid();
        UserId = userId;
        Name = name.Trim();
        Description = description.Trim();
        Target = target;
        AlreadySaved = 0m;
        TargetDate = targetDate;
        MonthlyContribution = monthlyContribution;
        Icon = icon;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void Update(
        string name,
        string description,
        decimal target,
        DateTime targetDate,
        decimal monthlyContribution,
        string icon)
    {
        EnsureTarget(target);
        EnsureTargetDate(targetDate);
        EnsureMonthlyContribution(monthlyContribution);

        Name = name.Trim();
        Description = description.Trim();
        Target = target;
        TargetDate = targetDate;
        MonthlyContribution = monthlyContribution;
        Icon = icon;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Contribute(decimal amount)
    {
        EnsureAmount(amount);
        AlreadySaved += amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw(decimal amount)
    {
        EnsureAmount(amount);
        if (amount > AlreadySaved)
            throw new DomainException("Amount cannot exceed the amount already saved.");

        AlreadySaved -= amount;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void EnsureTarget(decimal target)
    {
        if (target <= 0)
            throw new DomainException("Target must be greater than zero.");
    }

    private static void EnsureTargetDate(DateTime targetDate)
    {
        if (targetDate <= DateTime.UtcNow)
            throw new DomainException("Target date must be in the future.");
    }

    private static void EnsureMonthlyContribution(decimal monthlyContribution)
    {
        if (monthlyContribution < 0)
            throw new DomainException("Monthly contribution cannot be negative.");
    }

    private static void EnsureAmount(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Amount must be greater than zero.");
    }
}
