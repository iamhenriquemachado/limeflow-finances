using LimeFlow.API.Controllers;
using LimeFlow.API.Middlewares;
using LimeFlow.Application;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Services;
using LimeFlow.Infrastructure.Database;
using LimeFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();


var app = builder.Build();

app.UseExceptionHandler();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapUserEndpoints();
app.MapAccountEndpoints();

app.Run();