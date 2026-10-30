namespace LimeFlow.Application.Common.DTOs;

public record CreateUserRequest(string Name, string Email, string Password);

public record UpdateUserRequest(string Name);

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);

public record UserResponse(Guid Id, string Name, string Email, DateTime CreatedAt, DateTime UpdatedAt);
