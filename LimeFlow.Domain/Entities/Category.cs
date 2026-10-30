using LimeFlow.Domain.Enums;

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
        // TODO(Phase 4): throw DomainException when name is empty or longer than 50 characters.
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name.Trim();
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }

    public void Rename(string name)
    {
        // TODO(Phase 4): apply the same name rules as the constructor.
        Name = name.Trim();
    }
}
