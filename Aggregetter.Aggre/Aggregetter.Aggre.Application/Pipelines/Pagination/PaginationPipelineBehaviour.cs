using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Services.PaginationService;
using MediatR;
using System;
using System.Threading;

namespace Aggregetter.Aggre.Application.Pipelines.Pagination
{
    public sealed class PaginationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : IPaginationResponse
    {
        private readonly IPaginationService _paginationService;

        public PaginationPipelineBehaviour(IPaginationService paginationService)
        {
            _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));
        }
        public async System.Threading.Tasks.Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();

            var pagedUris = _paginationService.GetPagedUris(response.PageSize, response.Page, response.RecordCount);
            var recordCount = response.RecordCount;
            var pageSize = response.PageSize;
            var pageCount = recordCount % pageSize != 0
                ? recordCount / pageSize + 1
                : recordCount / pageSize;

            response.HasNextPage = pagedUris.NextPage;
            response.HasPreviousPage = pagedUris.PreviousPage;
            response.PageCount = pageCount;

            return response;
        }
    }
}
