using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.TranslateArticle
{
    public sealed class TranslateArticleCommandHandler : IRequestHandler<TranslateArticleCommand, TranslateArticleCommandResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ITranslationQueueService<Article> _translationQueueService;

        public TranslateArticleCommandHandler(IArticleRepository articleRepository,
            ITranslationQueueService<Article> messageQueueService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _translationQueueService = messageQueueService ?? throw new ArgumentNullException(nameof(messageQueueService));
        }

        public async Task<TranslateArticleCommandResponse> Handle(TranslateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken);
            
            var isSent = await _translationQueueService.Publish(article);
            if (isSent)
            {
                return new TranslateArticleCommandResponse()
                {
                    Message = "Sent to translation queue"
                };
            }

            throw new ApplicationException();
        }
    }
}
