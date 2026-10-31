using LimeFlow.Domain.Enums;
using LimeFlow.Domain.Exceptions;

namespace LimeFlow.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public TransactionType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Category() { }

    public Category(Guid userId, string name, TransactionType type)
    {
        EnsureName(name);

        Id = Guid.NewGuid();
        UserId = userId;
        Name = name.Trim();
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }

    public void Rename(string name)
    {
        EnsureName(name);
        Name = name.Trim();
    }

    private static void EnsureName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) ||  name.Trim().Length > 50) throw new DomainException("Name must be at most 50 characters.");
    }
}
