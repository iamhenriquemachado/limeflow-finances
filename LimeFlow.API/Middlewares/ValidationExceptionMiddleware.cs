using FluentValidation;

namespace LimeFlow.API.Middlewares
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ValidationExceptionMiddleware(RequestDelegate next)
        {

            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/problem+json";

                var errors = ex.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                var problemDetails = new HttpValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Detail = "One or more validation errors ocurred."
                };

                await context.Response.WriteAsJsonAsync(problemDetails);

            }
        }

    }
}
