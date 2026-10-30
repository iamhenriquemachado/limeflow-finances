using LimeFlow.Domain.Enums;

namespace LimeFlow.Domain.Entities;

public class Budget
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public decimal LimitAmount { get; private set; }
    public int AlertThreshold { get; private set; }
    public BudgetPeriod Period { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Budget() { }

    public Budget(
        Guid userId,
        Guid categoryId,
        decimal limitAmount,
        int alertThreshold,
        BudgetPeriod period,
        string? notes)
    {
        // TODO(Phase 6): throw DomainException when limitAmount <= 0
        // or alertThreshold is outside 1-100.
        Id = Guid.NewGuid();
        UserId = userId;
        CategoryId = categoryId;
        LimitAmount = limitAmount;
        AlertThreshold = alertThreshold;
        Period = period;
        Notes = notes?.Trim();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void Update(decimal limitAmount, int alertThreshold, BudgetPeriod period, string? notes)
    {
        // TODO(Phase 6): apply the same rules as the constructor.
        LimitAmount = limitAmount;
        AlertThreshold = alertThreshold;
        Period = period;
        Notes = notes?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}
