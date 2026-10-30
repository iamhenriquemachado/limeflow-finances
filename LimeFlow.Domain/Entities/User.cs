namespace LimeFlow.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private User() { }

    public User(string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Email = NormalizeEmail(email);
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

    public void UpdateProfile(string name)
    {
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }
}
