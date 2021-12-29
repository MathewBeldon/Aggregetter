using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.UriService;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public class GetArticlePagedListQueryHandler : IRequestHandler<GetArticlePagedListQuery, GetArticlePagedListQueryResponse>
    { 
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GetArticlePagedListQueryHandler(IBaseRepository<Article> articleRepository, 
            IMapper mapper,
            IUriService uriService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
        }

        public async Task<GetArticlePagedListQueryResponse> Handle(GetArticlePagedListQuery request, CancellationToken cancellationToken)
        {            
            var pagedResponse = await _articleRepository.GetPagedResponseAsync(request.PagedRequest.Page, request.PagedRequest.PageSize, cancellationToken);
            var totalAricles = await _articleRepository.GetCount();

            var getArticlePagedItemList = _mapper.Map<List<GetArticlePagedItemDto>>(pagedResponse);
            var pagedUris = _uriService.GetPagedUris(request.PagedRequest, "articles", totalAricles);

            return new GetArticlePagedListQueryResponse(getArticlePagedItemList)
            {
                PageSize = request.PagedRequest.PageSize,
                PageNumber = request.PagedRequest.Page,
                FirstPage = pagedUris.FirstUri,
                PreviousPage = pagedUris.PreviousUri,
                NextPage = pagedUris.NextUri,
                LastPage = pagedUris.LastUri,
                TotalRecords = totalAricles,
                TotalPages = (int)Math.Ceiling((double)totalAricles / request.PagedRequest.PageSize),
            };            
        }
    }
}
