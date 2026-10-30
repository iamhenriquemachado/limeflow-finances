using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Mappings;

public static class GoalMappings
{
    public static GoalResponse ToResponse(this Goal goal) =>
        new(
            goal.Id,
            goal.Name,
            goal.Description,
            goal.Target,
            goal.AlreadySaved,
            goal.TargetDate,
            goal.MonthlyContribution,
            goal.Icon,
            goal.ProgressPercent,
            goal.IsCompleted,
            goal.CreatedAt,
            goal.UpdatedAt);
}
