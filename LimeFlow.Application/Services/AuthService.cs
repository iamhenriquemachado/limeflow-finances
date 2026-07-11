using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;

namespace LimeFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordVerifier _verifier;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository repo, IPasswordVerifier verifier, ITokenService tokenService)
        {
            _repo = repo;
            _verifier = verifier;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct)
        {
            var userData = await _repo.GetByEmailAsync(request.email);

            if (userData is null)
            {
                return null;
            }

            var isValid = await _verifier.VerifyPassword(request.password, userData.Password);

            if (!isValid)
            {
                return null;
            }

            var token = _tokenService.GenerateToken(userData.Id, userData.Name, userData.Email);
            return new LoginResponseDto(token, "Bearer", 3600);
        }
    }
}
