using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LimeFlow.API.Controllers
{
    public static class UserEndpoints
    {

        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/users", async (IUserService service, ClaimsPrincipal user) =>
            {
                var claims = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var users = await service.GetUsersService();
                return Results.Ok(users);

            }).WithName("GetUsers")
            .WithSummary("List all registered users")
            .WithDescription("Retrieves a paginated list of all users available in the system. Requires administrative privileges.")
            .WithTags("Users")
            .Produces<List<UserResponseDto>>(StatusCodes.Status200OK, "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(void), "application/json")
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")
            .RequireAuthorization();


            group.MapGet("/users/{id:guid}", async (Guid id, IUserService service) =>
            {
                var user = await service.GetUserByIdService(id);
                return Results.Ok(user);

            }).WithName("GetUserById")
              .WithSummary("Get user by Id")
              .WithDescription("Return a user.")
              .WithTags("Users")
              .Produces<UserResponseDto>(StatusCodes.Status200OK)
              .Produces<UserResponseDto>(StatusCodes.Status404NotFound);

            group.MapGet("/users/me", async (ClaimsPrincipal user, IUserService service) =>
            {
                Guid userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var userData = await service.GetUserByIdService(userId);

                return Results.Ok(userData);
            }).WithName("Return user data")
            .WithSummary("Get user data")
            .WithDescription("Retrieves the profile data of the authenticated user based on their unique identifier (GUID).")
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .RequireAuthorization();


            group.MapPost("/users", async ([FromBody] CreateUserRequestDto request, IUserService service) =>
            {
                var user = await service.CreateUserService(request);
                return Results.Created($"api/v1/users/{user.Id}", user);


            }).WithName("CreateUser")
            .WithSummary("Create a new user")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status409Conflict);

            group.MapPut("/users", async ([FromBody] UpdateUserRequestDto request, IUserService service) =>
            {
                
            });

            group.MapDelete("/users/{id:guid}", async (Guid id, ClaimsPrincipal user, IUserService service) =>
            {
                var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (claim is null || !Guid.TryParse(claim, out var userId) || id != userId)
                    return Results.NotFound();

                await service.DeleteUserService(id);
                return Results.NoContent();


            }).WithName("DeleteUser")
              .WithSummary("Delete a user by Id")
              .WithDescription("Deletes a user and returns a 204 NoContent status response.")
              .WithTags("Users")
              .Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status404NotFound)
              .RequireAuthorization();


        }


    }
}
