using Aggregetter.Aggre.Application.Models.Base;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly ILogger<ValidationPipelineBehaviour<TRequest, TResponse>> _logger;
        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            if (!_validators.Any()) return await next(request, cancellationToken);

            var context = new ValidationContext<TRequest>(request);
            var validationResults = (await Task.WhenAll(_validators.Select(async query => await query.ValidateAsync(context, cancellationToken)))).SelectMany(x => x.Errors);

            if (validationResults.Any())
            {
                _logger.LogWarning("Validation error on {request}:\n{errors}", request.ToString(), validationResults.Select(x => "Input:" + x.AttemptedValue + " Message:" + x.ErrorMessage));
                throw new Exceptions.ValidationException(validationResults);
            }

            return await next(request, cancellationToken);
        }
    }
}
