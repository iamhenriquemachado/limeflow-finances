namespace LimeFlow.Application.Common.DTOs;

public record LoginRequest(string Email, string Password);

public record LoginResponse(string AccessToken, string TokenType, int ExpiresIn);
