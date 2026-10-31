using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Services;

public class AuthService : IAuthService
{
    private const string TokenType = "Bearer";

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByEmailAsync(User.NormalizeEmail(request.Email), ct);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");

        var token = _tokenService.GenerateToken(user);
        return new LoginResponse(token.Value, TokenType, token.ExpiresInSeconds);
    }
}
