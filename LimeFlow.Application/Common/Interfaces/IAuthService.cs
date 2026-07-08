using LimeFlow.Application.Common.DTOs;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct);
    }
}
