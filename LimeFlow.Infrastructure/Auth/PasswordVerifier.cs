using LimeFlow.Application.Common.Interfaces;


namespace LimeFlow.Infrastructure.Auth
{
    public class PasswordVerifier : IPasswordVerifier
    {
        public Task<bool> VerifyPassword(string password, string storedPasswordHashed)
        {
            bool passwordMatches = BCrypt.Net.BCrypt.EnhancedVerify(password, storedPasswordHashed);
            return Task.FromResult(passwordMatches);
        }
    }

}
