using Aggregetter.Aggre.Application.Models.Base;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Pipelines.Validation
{
    public sealed class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest  : IRequest<TResponse>
                                                                                                                  where TResponse : BaseResponse, new()
    {
        private readonly IValidator<TRequest> _validator;
        public ValidationPipelineBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await _validator.ValidateAsync(context, cancellationToken);

            if (validationResults.Errors.Count != 0) throw new ValidationException(validationResults.Errors);

            return await next();
        }
    }
}
