using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var invalidMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result<int>.GetFaulted), new[] { typeof(Exception) });
                var requestName = typeof(TRequest).Name;

                if (invalidMethod != null)
                {
                    return (TResponse)invalidMethod.Invoke(null, new object[] { new Exception("Validation error") });
                }

                throw new Exception("Internal error");
            }

            return await next();
        }
    }
}
