using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class ReportEndpoints
{
    public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/reports")
            .WithTags("Reports")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet("/summary", GetSummary)
            .WithName("GetReportSummary")
            .WithSummary("Dashboard totals for a date range");

        group.MapGet("/by-category", GetByCategory)
            .WithName("GetReportByCategory")
            .WithSummary("Totals grouped by category");

        group.MapGet("/monthly", GetMonthly)
            .WithName("GetMonthlyReport")
            .WithSummary("Income and expense per month for a year");

        return app;
    }

    private static async Task<Ok<ReportSummaryResponse>> GetSummary(
        DateTime? from,
        DateTime? to,
        IReportService reportService,
        CancellationToken ct)
    {
        var summary = await reportService.GetSummaryAsync(from, to, ct);
        return TypedResults.Ok(summary);
    }

    private static async Task<Ok<IReadOnlyList<CategoryReportItem>>> GetByCategory(
        DateTime? from,
        DateTime? to,
        TransactionType? type,
        IReportService reportService,
        CancellationToken ct)
    {
        var items = await reportService.GetByCategoryAsync(from, to, type, ct);
        return TypedResults.Ok(items);
    }

    private static async Task<Ok<IReadOnlyList<MonthlyReportItem>>> GetMonthly(
        int? year,
        IReportService reportService,
        CancellationToken ct)
    {
        var items = await reportService.GetMonthlyAsync(year ?? DateTime.UtcNow.Year, ct);
        return TypedResults.Ok(items);
    }
}
