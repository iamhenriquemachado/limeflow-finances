using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.DTOs;

public record CreateBudgetRequest(
    Guid CategoryId,
    decimal LimitAmount,
    int AlertThreshold,
    BudgetPeriod Period,
    string? Notes);

public record UpdateBudgetRequest(
    Guid CategoryId,
    decimal LimitAmount,
    int AlertThreshold,
    BudgetPeriod Period,
    string? Notes);

public record BudgetResponse(
    Guid Id,
    Guid CategoryId,
    decimal LimitAmount,
    int AlertThreshold,
    BudgetPeriod Period,
    string? Notes,
    DateTime CreatedAt,
    DateTime UpdatedAt);

public record BudgetStatusResponse(
    decimal LimitAmount,
    decimal Spent,
    decimal Remaining,
    int PercentUsed,
    bool IsOverThreshold,
    bool IsExceeded,
    DateTime PeriodStart,
    DateTime PeriodEnd);
