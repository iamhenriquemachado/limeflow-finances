using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
}
