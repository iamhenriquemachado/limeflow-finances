using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace LimeFlow.API.Controllers
{
    public static class UserEndpoints
    {

        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/users", async (IUserService service) =>
            {
                var users = await service.GetUsersService();
                return Results.Ok(users);

            }).WithName("GetUsers")
            .WithSummary("List all registered users")
            .WithDescription("Retrieves a paginated list of all users available in the system. Requires administrative privileges.")
            .WithTags("Users")
            .Produces<List<UserResponseDto>>(StatusCodes.Status200OK, "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(void), "application/json")
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json");


            group.MapGet("/users/{id:guid}", async (Guid id, IUserService service) =>
            {
                var user = await service.GetUserByIdService(id);
                return Results.Ok(user);

            }).WithName("GetUserById")
              .WithSummary("Get User By Id")
              .WithDescription("Return a user.")
              .WithTags("Users")
              .Produces<UserResponseDto>(StatusCodes.Status200OK)
              .Produces<UserResponseDto>(StatusCodes.Status404NotFound);


            group.MapPost("/users", async ([FromBody] CreateUserRequestDto request, IUserService service) =>
            {
                var user = await service.CreateUserService(request);
                return Results.Created($"api/v1/users/{user.Id}", user);


            }).WithName("CreateUser")
            .WithSummary("Create a New User")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest);

            group.MapDelete("/users/{id:guid}", async (Guid id, IUserService service) =>
            {
                await service.DeleteUserService(id);
                return Results.NoContent();

            }).WithName("DeleteUser")
              .WithSummary("Delete a User by id")
              .WithDescription("Deletes a user and returns a 204 NoContent status response.")
              .WithTags("Users")
              .Produces(StatusCodes.Status204NoContent);
        }


    }
}
