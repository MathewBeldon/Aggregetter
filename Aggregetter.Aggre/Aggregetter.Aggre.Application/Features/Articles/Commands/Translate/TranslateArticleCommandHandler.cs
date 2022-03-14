using Aggregetter.Aggre.Application.Contracts;
using Aggregetter.Aggre.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.Translate
{
    public sealed class TranslateArticleCommandHandler : IRequestHandler<TranslateArticleCommand, TranslateArticleCommandResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public TranslateArticleCommandHandler(IArticleRepository articleRepository,
            ILoggedInUserService loggedInUserService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
        }

        public async Task<TranslateArticleCommandResponse> Handle(TranslateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken);
            article.TranslatedBy = _loggedInUserService.UserId;
            article.TranslatedDateUtc = DateTime.UtcNow;

            _ =  Task.Run(() => _articleRepository.UpdateAsync(article, cancellationToken));

            return new TranslateArticleCommandResponse();
        }
    }
}
