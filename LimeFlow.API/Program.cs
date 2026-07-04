using LimeFlow.API.Controllers;
using LimeFlow.API.Middlewares;
using LimeFlow.Application;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Services;
using LimeFlow.Infrastructure.Database;
using LimeFlow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LimeFlow.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

var secret = builder.Configuration["JwtSettings:SecretKey"];
Console.WriteLine(secret);

if (string.IsNullOrWhiteSpace(secret))
{
    throw new Exception("JwtSettings:SecretKey não foi encontrada no appsettings.");
}


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"]
    };
});




builder.Services.AddAuthorization();


builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();


var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapAccountEndpoints();

app.Run();