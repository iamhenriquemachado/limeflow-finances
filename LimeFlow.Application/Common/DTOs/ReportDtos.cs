namespace LimeFlow.Application.Common.DTOs;

public record ReportSummaryResponse(
    decimal TotalBalance,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal Net);

public record CategoryReportItem(Guid CategoryId, string Name, decimal Total, decimal Percent);

public record MonthlyReportItem(int Month, decimal Income, decimal Expense);
