using LimeFlow.API.Controllers;
using LimeFlow.API.Middlewares;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Infrastructure.Database;
using LimeFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}



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