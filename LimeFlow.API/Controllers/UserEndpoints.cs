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
                return Results.Ok(users);
            }).WithName("GetUsers").WithSummary("Get all users")
            .WithDescription("Returns a list of users.")
            .WithTags("Users");


            group.MapPost("/users", async (CreateUserRequest user, IUserRepository repo) =>
            {
                int workFactor = 12;


                var errors = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(user.Name))
                    errors["name"] = ["Name is required."];

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor);

                var newUserEntity = new User
                {
                    Id = Guid.NewGuid(),
                    Email = user.Email,
                    Name = user.Name,
                    Password = hashedPassword,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                };

                await repo.CreateAsync(newUserEntity);

                return Results.Created($"/api/v1/users/{newUserEntity.Id}", newUserEntity);

            }).WithName("CreateUser")
            .WithSummary("Create a new user")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users");
        }
    }
}
