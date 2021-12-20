using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public class GetArticlePagedListQueryHandler : IRequestHandler<GetArticlePagedListQuery, ArticlePagedListResponse>
    { 
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlePagedListQueryHandler(IBaseRepository<Article> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        public async Task<ArticlePagedListResponse> Handle(GetArticlePagedListQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleRepository.GetPagedResponseAsync(request.page, cancellationToken);
            var articlePagedListDto = _mapper.Map<List<ArticlePagedItemDto>>(result);

            return new ArticlePagedListResponse(request.page, 20, articlePagedListDto);
        }
    }
}
