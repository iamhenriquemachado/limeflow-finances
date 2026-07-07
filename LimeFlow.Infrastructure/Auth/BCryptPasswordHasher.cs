using BCrypt.Net;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LimeFlow.Infrastructure.Auth
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string PasswordHasher(string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);
            return passwordHash;

        }
    }


}
