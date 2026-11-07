using LimeFlow.Domain.Enums;
using LimeFlow.Domain.Exceptions;

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
        EnsureAmount(amount);
        EnsureTransactionDate(transactionDate);

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
        EnsureAmount(amount);
        EnsureTransactionDate(transactionDate);

        AccountId = accountId;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        Description = description.Trim();
        TransactionDate = transactionDate;
    }

    private static void EnsureAmount(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Amount must be greater than zero.");
    }

    private static void EnsureTransactionDate(DateTime transactionDate)
    {
        if (transactionDate > DateTime.UtcNow)
            throw new DomainException("Transaction date cannot be in the future.");
    }
}
