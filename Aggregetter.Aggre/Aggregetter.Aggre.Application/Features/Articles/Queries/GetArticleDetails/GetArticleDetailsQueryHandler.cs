using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, GetArticleDetailsQueryResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleDetailsQueryHandler(IArticleRepository articleRepository,
                                             IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<GetArticleDetailsQueryResponse> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken);

            var articleDetails = _mapper.Map<GetArticleDetailsDto>(article);

            return new GetArticleDetailsQueryResponse(articleDetails);
        }
    }
}
