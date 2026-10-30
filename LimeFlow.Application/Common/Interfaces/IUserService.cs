using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken ct = default);
    Task<UserResponse> GetCurrentAsync(CancellationToken ct = default);
    Task<UserResponse> UpdateCurrentAsync(UpdateUserRequest request, CancellationToken ct = default);
    Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken ct = default);
    Task DeleteCurrentAsync(CancellationToken ct = default);
}
