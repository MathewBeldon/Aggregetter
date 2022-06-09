using Aggregetter.Aggre.Application.Models.Base;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Pipelines.Validation
{
    public sealed class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                                  where TResponse : BaseResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) return await next();            

            var context = new ValidationContext<TRequest>(request);
            var validationResults = (await Task.WhenAll(_validators.Select(async query => await query.ValidateAsync(context, cancellationToken)))).SelectMany(x => x.Errors);

            if (validationResults.Any()) throw new Exceptions.ValidationException(validationResults);            

            return await next();
        }
    }
}
