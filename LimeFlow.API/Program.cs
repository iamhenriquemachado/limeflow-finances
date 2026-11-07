using LimeFlow.API;
using LimeFlow.API.Endpoints;
using LimeFlow.Application;
using LimeFlow.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.Use(async (context, next) =>
{
    context.Response.OnStarting(() =>
    {
        var path = context.Request.Path.Value ?? "/";
        if (path is "/" or "/index.html")
        {
            context.Response.Headers.ContentSecurityPolicy =
                "default-src 'self'; script-src 'self'; style-src 'self' https://fonts.googleapis.com; style-src-attr 'unsafe-inline'; font-src https://fonts.gstatic.com; connect-src 'self'; img-src 'self'; base-uri 'self'; form-action 'self'; frame-ancestors 'none'";
        }

        return Task.CompletedTask;
    });

    await next();

    // #region agent log
    var apiPath = context.Request.Path.Value ?? "";
    if (apiPath.StartsWith("/api", StringComparison.Ordinal))
    {
        try
        {
            var payload = System.Text.Json.JsonSerializer.Serialize(new
            {
                sessionId = "b6e95b",
                hypothesisId = "H1",
                location = "Program.cs:request",
                message = "api response",
                data = new
                {
                    method = context.Request.Method,
                    path = apiPath,
                    status = context.Response.StatusCode,
                    authenticated = context.User.Identity?.IsAuthenticated == true
                },
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
            System.IO.File.AppendAllText(@"c:\Users\heyhe\development\limeflow-finances\.cursor\debug-b6e95b.log", payload + "\n");
        }
        catch
        {
        }
    }
    // #endregion
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();

app.Run();
