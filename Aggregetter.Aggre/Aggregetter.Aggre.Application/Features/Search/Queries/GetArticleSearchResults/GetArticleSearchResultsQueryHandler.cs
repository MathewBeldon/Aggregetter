using Aggregetter.Aggre.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
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

        public async Task<GetArticleSearchResultsQueryResponse> Handle(GetArticleSearchResultsQuery request, CancellationToken cancellationToken)
        {
            //var searchResultCount = await _articleRepository.GetSearchResultCountAsync(request.SearchString, cancellationToken);
            var articleSearchResultEntities = await _articleRepository.GetSearchResultsAsync(request.Page, request.PageSize, request.SearchString, cancellationToken);
            var articleDtos = _mapper.Map<List<GetArticleSearchResultsDto>>(articleSearchResultEntities);

            return new GetArticleSearchResultsQueryResponse(data: articleDtos, page: request.Page, pageSize: request.PageSize, recordCount: 111);
        }
    }
}
