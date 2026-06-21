using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Behaviours
{
    internal class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = results.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
