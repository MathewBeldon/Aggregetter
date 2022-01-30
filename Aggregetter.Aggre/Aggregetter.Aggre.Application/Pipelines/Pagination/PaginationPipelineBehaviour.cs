using MediatR;
using System.Threading;

namespace Aggregetter.Aggre.Application.Pipelines.Pagination
{
    public sealed class PaginationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IPaginationRequest
    {
        public System.Threading.Tasks.Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}
