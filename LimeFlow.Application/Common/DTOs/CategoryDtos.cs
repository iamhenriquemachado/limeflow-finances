using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.DTOs;

public record CreateCategoryRequest(string Name, TransactionType Type);

public record UpdateCategoryRequest(string Name);

public record CategoryResponse(Guid Id, string Name, TransactionType Type, DateTime CreatedAt);
