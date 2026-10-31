using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.Interfaces;
using System.Text.Json.Serialization;

namespace LimeFlow.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info.Title = "LimeFlow API";
                document.Info.Version = "v1";
                document.Info.Description = """
            ## Introduction

            LimeFlowAPI is a personal finance management API designed to help users track bank accounts, manage income and expenses, create budgets, and monitor savings goals. 
            
            It provides a centralized way to organize financial data and gain better control over personal finances.
            """;
                return Task.CompletedTask;
            });
        });

        return services;
    }
}
