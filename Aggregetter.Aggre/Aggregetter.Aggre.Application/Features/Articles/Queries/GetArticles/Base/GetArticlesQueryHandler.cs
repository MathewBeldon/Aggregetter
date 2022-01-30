using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.PaginationService;
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
        private readonly IPaginationService _paginationService;

        public GetArticlesQueryHandler(IArticleRepository articleRepository, 
            IMapper mapper,
            IPaginationService paginationService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));
        }

        public async Task<GetArticlesQueryResponse> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var totalArticles = await _articleRepository.GetCount(cancellationToken);
            var getArticlesRequest = await _articleRepository.GetArticlesPagedAsync(request.Page, request.PageSize, totalArticles, cancellationToken);

            var getArticlesDtoList = _mapper.Map<List<GetArticlesDto>>(getArticlesRequest);
            var pagedUris = _paginationService.GetPagedUris(request.PageSize, request.Page, totalArticles);

            return new GetArticlesQueryResponse(getArticlesDtoList)
            {
                PageSize = request.PageSize,
                PageNumber = request.Page,
                HasPreviousPage = pagedUris.PreviousPage,
                HasNextPage = pagedUris.NextPage,
                TotalRecords = totalArticles,
                TotalPages = (int)Math.Ceiling((double)totalArticles / request.PageSize),
            };            
        }
    }
}
