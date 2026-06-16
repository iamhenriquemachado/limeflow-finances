using LimeFlow.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var users = new List<User>
{
    new User("Henrique Machado", "heyhenriquecastro@gmail.com", "Admin"),
    new User("John Dooe", "johndoe@company.com", "Manager"),
};

app.MapGet("/", () =>  "Hello World");