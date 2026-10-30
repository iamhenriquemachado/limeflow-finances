using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/sessions")
            .WithTags("Authentication")
            .AllowAnonymous();

        group.MapPost("/", Login)
            .WithName("Login")
            .WithSummary("Authenticate a user")
            .WithDescription("Validates the credentials and returns a JWT for accessing protected resources.")
            .WithValidation<LoginRequest>()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        return app;
    }

    private static async Task<Ok<LoginResponse>> Login(
        LoginRequest request,
        IAuthService authService,
        CancellationToken ct)
    {
        var response = await authService.LoginAsync(request, ct);
        return TypedResults.Ok(response);
    }
}
