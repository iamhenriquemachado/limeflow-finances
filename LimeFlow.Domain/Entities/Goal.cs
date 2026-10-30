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
        // TODO(Phase 7): throw DomainException when target <= 0
        // or targetDate is not in the future.
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
        // TODO(Phase 7): apply the same rules as the constructor.
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
        // TODO(Phase 7): reject amount <= 0, add to AlreadySaved, update UpdatedAt.
        throw new NotImplementedException();
    }

    public void Withdraw(decimal amount)
    {
        // TODO(Phase 7): reject amount <= 0 or amount > AlreadySaved, subtract, update UpdatedAt.
        throw new NotImplementedException();
    }
}
