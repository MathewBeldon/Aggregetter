using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Exceptions;
using AutoMapper;
using Mediator;
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

        public async ValueTask<GetArticleDetailsQueryResponse> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken) ??
                throw new RecordNotFoundException($"Article {request.ArticleSlug}");
            
            var articleDetails = _mapper.Map<GetArticleDetailsDto>(article);

            return new GetArticleDetailsQueryResponse(articleDetails);
        }
    }
}
