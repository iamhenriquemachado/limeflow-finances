namespace LimeFlow.Application.Common.DTOs;

public record CreateAccountRequest(string Name, string Bank);

public record UpdateAccountRequest(string Name, string Bank);

public record AccountResponse(
    Guid Id,
    string Name,
    string Bank,
    decimal Balance,
    DateTime CreatedAt,
    DateTime UpdatedAt);
