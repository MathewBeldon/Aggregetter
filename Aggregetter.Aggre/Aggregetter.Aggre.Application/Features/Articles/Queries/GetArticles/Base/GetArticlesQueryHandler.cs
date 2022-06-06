using Aggregetter.Aggre.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, GetArticlesQueryResponse>
    { 
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesQueryHandler(IMapper mapper,
            IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetArticlesQueryResponse> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articleCount = await _articleRepository.GetCount(cancellationToken);
            var articleEntities = await _articleRepository.GetArticlesPagedAsync(request.Page, request.PageSize, articleCount, cancellationToken);
            var articleDtos = _mapper.Map<List<GetArticlesDto>>(articleEntities);

            return new GetArticlesQueryResponse(articleDtos)
            {
                Page = request.Page,
                PageSize = request.PageSize,
                PageCount = articleCount
            };          
        }
    }
}
