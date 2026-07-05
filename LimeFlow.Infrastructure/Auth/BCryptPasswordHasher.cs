using BCrypt.Net;

namespace LimeFlow.Infrastructure.Auth
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public async Task<string> PasswordHasher(string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);
            return passwordHash;

        }
    }

    public interface IPasswordHasher
    {
        Task<string> PasswordHasher(string password);
    }
}
