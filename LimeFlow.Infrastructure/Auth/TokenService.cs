using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Infrastructure.Auth
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string userId, string email, IEnumerable<string> roles)
        {
            
        }
    }

    public interface ITokenService
    {
        string GenerateToken(string userId, string email, IEnumerable<string> roles);
    }
}
