using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordVerifier _verifier;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<LoginResposeDto?> LoginAsync(LoginRequestDto request, CancellationToken ct)
        {
            var userData = await _repo.GetByEmailAsync(request.email);

            var validatePasswordHash = await _verifier.VerifyPassword(request.password, userData.Password);

            if (!validatePasswordHash)
            {
                throw new Exception("Invalid Credentials.");
            }

            var token = _tokenService.GenerateToken(request);

            return new LoginResposeDto(token, "Bearer", 3600); 

        }
    }
}
