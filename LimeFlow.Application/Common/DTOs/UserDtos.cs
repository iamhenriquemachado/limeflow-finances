namespace LimeFlow.Application.Common.DTOs
{
    public record UserDataResponseDto(Guid Id, string Email, string Password, string Name, DateTime CreatedAt);

    public record UserResponseDto(Guid Id, string Email, string Name, DateTime CreatedAt);
    public record CreateUserRequestDto(string Name, string Email, string Password);
    public record CreatedUserResponseDto(Guid UserId, string Email);
    public record UpdateUserRequestDto (string Name, string Password);
}
