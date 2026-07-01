namespace LimeFlow.API.Controllers
{
    public static class AuthEndpoints
    {

        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/v1/");
        }
    }
}
