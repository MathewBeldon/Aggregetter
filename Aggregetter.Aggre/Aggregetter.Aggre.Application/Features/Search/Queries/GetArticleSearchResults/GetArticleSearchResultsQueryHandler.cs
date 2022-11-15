using Aggregetter.Aggre.Application.Contracts.Persistence;
using AutoMapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQueryHandler : IRequestHandler<GetArticleSearchResultsQuery, GetArticleSearchResultsQueryResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleSearchResultsQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public ValueTask<GetArticleSearchResultsQueryResponse> Handle(GetArticleSearchResultsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
