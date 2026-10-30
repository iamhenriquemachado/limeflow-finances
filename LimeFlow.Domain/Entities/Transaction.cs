using LimeFlow.Domain.Enums;

namespace LimeFlow.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid CategoryId { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime TransactionDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Transaction() { }

    public Transaction(
        Guid accountId,
        Guid categoryId,
        TransactionType type,
        decimal amount,
        string description,
        DateTime transactionDate)
    {
        // TODO(Phase 5): throw DomainException when amount <= 0
        // or when transactionDate > DateTime.UtcNow ("Transaction date cannot be in the future").
        Id = Guid.NewGuid();
        AccountId = accountId;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        Description = description.Trim();
        TransactionDate = transactionDate;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        Guid accountId,
        Guid categoryId,
        TransactionType type,
        decimal amount,
        string description,
        DateTime transactionDate)
    {
        // TODO(Phase 5): apply the same rules as the constructor.
        // The balance revert/apply is orchestrated by TransactionService, not here.
        AccountId = accountId;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        Description = description.Trim();
        TransactionDate = transactionDate;
    }
}
