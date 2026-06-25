using FluentValidation;
using LimeFlow.API.Controllers;
using LimeFlow.API.Middlewares;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Infrastructure.Database;
using LimeFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using LimeFlow.Application; // Add this using directive to resolve ApplicationAssemblyReference

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();  
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
    cfg.AddOpenBehavior(typeof(LimeFlow.Application.Behaviours.ValidationBehavior<,>));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseExceptionHandler();

// Open API and Scalar Docs
app.MapOpenApi();
app.MapScalarApiReference();

// Endpoints Register
app.MapUserEndpoints();

app.Run();

// Add this class in your Application project (e.g., LimeFlow.Application) if it does not exist.
// This is a marker type used for assembly scanning by FluentValidation.
namespace LimeFlow.Application
{
    public sealed class ApplicationAssemblyReference
    {
    }
}