using LimeFlow.Domain.Models.Settings;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Infrastructure.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;


        public TokenService(IOptions<JwtSettings> settings) => _settings = settings.Value;
        public async Task<string> GenerateToken(LoginRequestDto request)
        {
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, request.email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor).ToString();

        }
    }

    public interface ITokenService
    {
        Task<string> GenerateToken(LoginRequestDto request);
    }
}
