using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Infrastructure.Auth;
using LimeFlow.Infrastructure.Persistence;
using LimeFlow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LimeFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddJwtAuthentication(configuration);

        return services;
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
    }

    private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(JwtSettings.SectionName);
        var jwtSettings = section.Get<JwtSettings>()
            ?? throw new InvalidOperationException($"Section '{JwtSettings.SectionName}' is not configured.");

        if (jwtSettings.SecretKey.Length < JwtSettings.MinimumSecretKeyLength)
            throw new InvalidOperationException(
                $"'{JwtSettings.SectionName}:SecretKey' must have at least {JwtSettings.MinimumSecretKeyLength} characters. " +
                "Set it with 'dotnet user-secrets'.");

        services.Configure<JwtSettings>(section);
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

        services.AddAuthorization();
    }
}
