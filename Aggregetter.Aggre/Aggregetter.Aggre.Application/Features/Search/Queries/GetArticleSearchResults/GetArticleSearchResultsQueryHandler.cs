using Aggregetter.Aggre.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQueryHandler : IRequestHandler<GetArticleSearchResultsQuery, GetArticleSearchResultsQueryResponse>
    {
        private readonly IArticleRepository _articleRepository;

        public GetArticleSearchResultsQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }

        public Task<GetArticleSearchResultsQueryResponse> Handle(GetArticleSearchResultsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
