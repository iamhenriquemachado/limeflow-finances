using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common;

public static class BudgetPeriods
{
    public static (DateTime Start, DateTime End) GetCurrentRange(BudgetPeriod period, DateTime utcNow)
    {
        var instant = utcNow.Kind == DateTimeKind.Utc ? utcNow : utcNow.ToUniversalTime();
        var (start, next) = period switch
        {
            BudgetPeriod.Weekly => Week(instant),
            BudgetPeriod.Monthly => Month(instant),
            BudgetPeriod.Yearly => Year(instant),
            _ => throw new ArgumentOutOfRangeException(nameof(period), period, "Budget period is not defined.")
        };

        return (start, next.AddSeconds(-1));
    }

    private static (DateTime Start, DateTime Next) Week(DateTime utcNow)
    {
        var startOfDay = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);
        var daysFromMonday = ((int)startOfDay.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        var start = startOfDay.AddDays(-daysFromMonday);
        return (start, start.AddDays(7));
    }

    private static (DateTime Start, DateTime Next) Month(DateTime utcNow)
    {
        var start = new DateTime(utcNow.Year, utcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        return (start, start.AddMonths(1));
    }

    private static (DateTime Start, DateTime Next) Year(DateTime utcNow)
    {
        var start = new DateTime(utcNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (start, start.AddYears(1));
    }
}
