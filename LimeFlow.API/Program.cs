using LimeFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Infrastructure.Repositories;
using LimeFlow.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

app.UseExceptionHandler();

// Open API and Scalar Docs
app.MapOpenApi();
app.MapScalarApiReference();

// Endpoints Register
app.MapUserEndpoints();

app.Run();