using LimeFlow.Domain.Enums;
using LimeFlow.Domain.Exceptions;

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
        EnsureLimit(limitAmount);
        EnsureThreshold(alertThreshold);

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

    public void Update(Guid categoryId, decimal limitAmount, int alertThreshold, BudgetPeriod period, string? notes)
    {
        EnsureLimit(limitAmount);
        EnsureThreshold(alertThreshold);

        CategoryId = categoryId;
        LimitAmount = limitAmount;
        AlertThreshold = alertThreshold;
        Period = period;
        Notes = notes?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    private static void EnsureLimit(decimal limitAmount)
    {
        if (limitAmount <= 0)
            throw new DomainException("Limit amount must be greater than zero.");
    }

    private static void EnsureThreshold(int alertThreshold)
    {
        if (alertThreshold is < 1 or > 100)
            throw new DomainException("Alert threshold must be between 1 and 100.");
    }
}
