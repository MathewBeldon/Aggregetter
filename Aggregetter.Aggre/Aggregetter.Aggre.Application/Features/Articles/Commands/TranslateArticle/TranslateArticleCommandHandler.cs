using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.TranslateArticle
{
    public sealed class TranslateArticleCommandHandler : IRequestHandler<TranslateArticleCommand, TranslateArticleCommandResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMessagePublishService<Article> _messagePublishService;

        public TranslateArticleCommandHandler(IArticleRepository articleRepository,
            IMessagePublishService<Article> messageQueueService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _messagePublishService = messageQueueService ?? throw new ArgumentNullException(nameof(messageQueueService));
        }

        public async ValueTask<TranslateArticleCommandResponse> Handle(TranslateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken);
            
            var isSent = await _messagePublishService.Publish(article, cancellationToken);
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
