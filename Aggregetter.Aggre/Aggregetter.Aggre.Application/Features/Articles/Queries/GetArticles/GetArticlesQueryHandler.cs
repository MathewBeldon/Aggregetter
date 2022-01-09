using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.UriService;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, GetArticlesQueryResponse>
    { 
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GetArticlesQueryHandler(IBaseRepository<Article> articleRepository, 
            IMapper mapper,
            IUriService uriService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
        }

        public async Task<GetArticlesQueryResponse> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {            
            var pagedResponse = await _articleRepository.GetPagedResponseAsync(request.PagedRequest.Page, request.PagedRequest.PageSize, cancellationToken);
            var totalArticles = await _articleRepository.GetCount();

            var getArticleList = _mapper.Map<List<ArticleDto>>(pagedResponse);
            var pagedUris = _uriService.GetPagedUris(request.PagedRequest, "articles", totalArticles);

            return new GetArticlesQueryResponse(getArticleList)
            {
                PageSize = request.PagedRequest.PageSize,
                PageNumber = request.PagedRequest.Page,
                FirstPage = pagedUris.FirstUri,
                PreviousPage = pagedUris.PreviousUri,
                NextPage = pagedUris.NextUri,
                LastPage = pagedUris.LastUri,
                TotalRecords = totalArticles,
                TotalPages = (int)Math.Ceiling((double)totalArticles / request.PagedRequest.PageSize),
            };            
        }
    }
}
