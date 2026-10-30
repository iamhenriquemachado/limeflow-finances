using LimeFlow.Domain.Exceptions;

namespace LimeFlow.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Bank { get; private set; } = string.Empty;
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Account() { }

    public Account(string name, string bank, Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name.Trim();
        Bank = bank.Trim();
        Balance = 0m;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void AddCredit(decimal amount)
    {
        EnsurePositive(amount);
        Balance += amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Debit(decimal amount)
    {
        EnsurePositive(amount);
        if (amount > Balance) throw new DomainException("Insufficient funds.");

        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Rename(string name, string bank)
    {
        Name = name.Trim();
        Bank = bank.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    private static void EnsurePositive(decimal amount)
    {
        if (amount <= 0) throw new DomainException("Amount must be greater than zero.");
    }
}
