﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory
{
    public sealed class GetArticlesByProviderAndCategoryQueryHandler : IRequestHandler<GetArticlesByProviderAndCategoryQuery, GetArticlesQueryResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesByProviderAndCategoryQueryHandler(IArticleRepository articleRepository,
            IMapper mapper)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetArticlesQueryResponse> Handle(GetArticlesByProviderAndCategoryQuery request, CancellationToken cancellationToken)
        {
            var articleCount = await _articleRepository.GetCountByProviderAndCategory(request.ProviderId, request.CategoryId, cancellationToken);
            var articleEntities = await _articleRepository.GetArticlesByProviderAndCategoryPagedAsync(request.Page, request.PageSize, request.ProviderId, request.CategoryId, cancellationToken);
            var articleDtos = _mapper.Map<List<GetArticlesDto>>(articleEntities);

            return new GetArticlesQueryResponse(data: articleDtos, page: request.Page, pageSize: request.PageSize, recordCount: articleCount);
        }
    }
}