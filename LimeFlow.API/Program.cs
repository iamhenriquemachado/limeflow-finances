using LimeFlow.API.Controllers;
using LimeFlow.API.Middlewares;
using LimeFlow.Application;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Services;
using LimeFlow.Domain.Models.Settings;
using LimeFlow.Infrastructure.Auth;
using LimeFlow.Infrastructure.Database;
using LimeFlow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var secret = builder.Configuration["JwtSettings:SecretKey"];

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

    
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
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
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IPasswordVerifier, PasswordVerifier>();
builder.Services.AddScoped<IAuthService, AuthService>();


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