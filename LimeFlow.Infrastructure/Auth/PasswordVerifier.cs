using LimeFlow.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
