using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleCommandResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public CreateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async ValueTask<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<Article>(request);
            var response = await _articleRepository.AddAsync(article, cancellationToken);
            
            var createArticleDto = _mapper.Map<CreateArticleDto>(response);

            return new CreateArticleCommandResponse(createArticleDto);
        }
    }
}
