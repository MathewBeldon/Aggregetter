﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using AutoMapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public class GetArticlesByCategoryQueryHandler : IRequestHandler<GetArticlesByCategoryQuery, GetArticlesQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;

        public GetArticlesByCategoryQueryHandler(IMapper mapper,
            IArticleRepository articleRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }

        public async ValueTask<GetArticlesQueryResponse> Handle(GetArticlesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var articleCount = await _articleRepository.GetCountByCategory(request.CategoryId, cancellationToken);            
            var articleEntities = await _articleRepository.GetArticlesByCategoryPagedAsync(request.Page, request.PageSize, request.CategoryId, cancellationToken);
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
