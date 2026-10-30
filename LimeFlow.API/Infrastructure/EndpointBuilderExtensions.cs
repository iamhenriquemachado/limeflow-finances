namespace LimeFlow.API.Infrastructure;

public static class EndpointBuilderExtensions
{
    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder) where TRequest : class =>
        builder
            .AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
}
