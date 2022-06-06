using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory
{
    public sealed class GetArticlesByProviderAndCategoryQueryHandler : IRequestHandler<GetArticlesByProviderAndCategoryQuery, GetArticlesQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;

        public GetArticlesByProviderAndCategoryQueryHandler(IMapper mapper,
            IArticleRepository articleRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }

        public async Task<GetArticlesQueryResponse> Handle(GetArticlesByProviderAndCategoryQuery request, CancellationToken cancellationToken)
        {
            var articleCount = await _articleRepository.GetCountByProviderAndCategory(request.ProviderId, request.CategoryId, cancellationToken);
            var articleEntities = await _articleRepository.GetArticlesByProviderAndCategoryPagedAsync(request.Page, request.PageSize, request.ProviderId, request.CategoryId, cancellationToken);
            var articleDtos = _mapper.Map<List<GetArticlesDto>>(articleEntities);

            return new GetArticlesQueryResponse(articleDtos)
            {
                Page = request.Page,
                PageSize = request.PageSize,
                RecordCount = articleCount
            };
        }
    }
}
