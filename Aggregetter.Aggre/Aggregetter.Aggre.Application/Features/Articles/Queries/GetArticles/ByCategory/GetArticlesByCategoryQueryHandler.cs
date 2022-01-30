using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.PaginationService;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    internal class GetArticlesByCategoryQueryHandler : IRequestHandler<GetArticlesByCategoryQuery, GetArticlesByCategoryQueryResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        public GetArticlesByCategoryQueryHandler(IArticleRepository articleRepository,
            IMapper mapper,
            IPaginationService paginationService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));
        }

        public async Task<GetArticlesByCategoryQueryResponse> Handle(GetArticlesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var totalArticles = await _articleRepository.GetCountByCategory(request.CategoryId, cancellationToken);
            var getArticlesRequest = await _articleRepository.GetArticlesByCategoryPagedAsync(request.Page, request.PageSize, request.CategoryId, cancellationToken);

            var getArticlesDtoList = _mapper.Map<List<GetArticlesDto>>(getArticlesRequest);
            
            var pagedUris = _paginationService.GetPagedUris(request.PageSize, request.Page, totalArticles);

            return new GetArticlesByCategoryQueryResponse(getArticlesDtoList)
            {                
                PageSize = request.PageSize,
                PageNumber = request.Page,
                HasPreviousPage = pagedUris.PreviousPage,
                HasNextPage = pagedUris.NextPage,
                TotalRecords = totalArticles,
                TotalPages = (int)Math.Ceiling((double)totalArticles / request.PageSize) - 1,
            };
        }
    }
}
