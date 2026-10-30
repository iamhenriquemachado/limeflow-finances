namespace LimeFlow.API.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapUserEndpoints();
        app.MapAccountEndpoints();
        app.MapCategoryEndpoints();
        app.MapTransactionEndpoints();
        app.MapBudgetEndpoints();
        app.MapGoalEndpoints();
        app.MapReportEndpoints();

        return app;
    }
}
