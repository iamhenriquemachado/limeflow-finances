using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.DTOs;
using LimeFlow.Domain.Models;
using System.Runtime.CompilerServices;
using BCrypt.Net;

namespace LimeFlow.API.Controllers
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/users", async (IUserRepository repo) =>
            {
                var users = await repo.GetAllAsync();


                var userResponseDto = users.Select(user => new UserResponseDto(
                    user.Id,
                    user.Email,
                    user.Name,
                    user.CreatedAt,
                    user.LastUpdatedAt
                 )).ToList();

                return Results.Ok(userResponseDto);

            }).WithName("GetUsers")
            .WithSummary("Get all users")
            .WithDescription("Returns a list of users.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status200OK);


            group.MapPost("/users", async (CreateUserRequest request, IUserRepository repo) =>
            {
                int workFactor = 12;


                var errors = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(request.Name))
                    errors["name"] = ["Name is required."];
                if (request.Name?.Length > 100)
                    errors["name"] = ["Cannot exceed 100 characters for the name"];
                if (string.IsNullOrWhiteSpace(request.Password))
                    errors["password"] = ["Password cannot be null or empty"];

                if (errors.Count > 0)
                {
                    return Results.ValidationProblem(errors);
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor);

                var newUserEntity = new User
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Name = request.Name,
                    Password = hashedPassword,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                };

                var userResponseDto = new UserResponseDto
                (
                    newUserEntity.Id,
                    newUserEntity.Email,
                    newUserEntity.Name,
                    newUserEntity.CreatedAt,
                    newUserEntity.LastUpdatedAt
                );

                await repo.CreateAsync(newUserEntity);

                return Results.Created($"/api/v1/users/{newUserEntity.Id}", userResponseDto);

            }).WithName("CreateUser")
            .WithSummary("Create a new user")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest);
        }
    }
}
