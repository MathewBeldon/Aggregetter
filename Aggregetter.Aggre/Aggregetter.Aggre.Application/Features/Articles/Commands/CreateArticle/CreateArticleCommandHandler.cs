using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Exceptions;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
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

        public async Task<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var createArticleCommandResponse = new CreateArticleCommandResponse();

                var article = _mapper.Map<Article>(request);
                var response = await _articleRepository.AddAsync(article, cancellationToken);
                createArticleCommandResponse.Data = _mapper.Map<CreateArticleDto>(response);

                return createArticleCommandResponse;            

        }
    }
}
