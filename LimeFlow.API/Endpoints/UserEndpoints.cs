using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/users")
            .WithTags("Users")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapPost("/", Create)
            .AllowAnonymous()
            .WithName("CreateUser")
            .WithSummary("Register a new user")
            .WithValidation<CreateUserRequest>()
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapGet("/me", GetCurrent)
            .WithName("GetCurrentUser")
            .WithSummary("Get the authenticated user's profile")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPut("/me", UpdateCurrent)
            .WithName("UpdateCurrentUser")
            .WithSummary("Update the authenticated user's profile")
            .WithValidation<UpdateUserRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPatch("/me/password", ChangePassword)
            .WithName("ChangeCurrentUserPassword")
            .WithSummary("Change the authenticated user's password")
            .WithValidation<ChangePasswordRequest>();

        group.MapDelete("/me", DeleteCurrent)
            .WithName("DeleteCurrentUser")
            .WithSummary("Delete the authenticated user and all their data")
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }

    private static async Task<Created<UserResponse>> Create(
        CreateUserRequest request,
        IUserService userService,
        CancellationToken ct)
    {
        var user = await userService.CreateAsync(request, ct);
        return TypedResults.Created("/api/v1/users/me", user);
    }

    private static async Task<Ok<UserResponse>> GetCurrent(IUserService userService, CancellationToken ct)
    {
        var user = await userService.GetCurrentAsync(ct);
        return TypedResults.Ok(user);
    }

    private static async Task<Ok<UserResponse>> UpdateCurrent(
        UpdateUserRequest request,
        IUserService userService,
        CancellationToken ct)
    {
        var user = await userService.UpdateCurrentAsync(request, ct);
        return TypedResults.Ok(user);
    }

    private static async Task<NoContent> ChangePassword(
        ChangePasswordRequest request,
        IUserService userService,
        CancellationToken ct)
    {
        await userService.ChangePasswordAsync(request, ct);
        return TypedResults.NoContent();
    }

    private static async Task<NoContent> DeleteCurrent(IUserService userService, CancellationToken ct)
    {
        await userService.DeleteCurrentAsync(ct);
        return TypedResults.NoContent();
    }
}
