namespace LimeFlow.Application.Common.DTOs;

public record CreateGoalRequest(
    string Name,
    string Description,
    decimal Target,
    DateTime TargetDate,
    decimal MonthlyContribution,
    string Icon);

public record UpdateGoalRequest(
    string Name,
    string Description,
    decimal Target,
    DateTime TargetDate,
    decimal MonthlyContribution,
    string Icon);

public record GoalContributionRequest(decimal Amount, Guid? FromAccountId);

public record GoalWithdrawalRequest(decimal Amount);

public record GoalResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Target,
    decimal AlreadySaved,
    DateTime TargetDate,
    decimal MonthlyContribution,
    string Icon,
    decimal ProgressPercent,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime UpdatedAt);
