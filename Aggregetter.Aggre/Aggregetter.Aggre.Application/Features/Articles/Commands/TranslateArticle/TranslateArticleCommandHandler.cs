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
        private readonly IMessageQueueService<Article> _messageQueueService;

        public TranslateArticleCommandHandler(IArticleRepository articleRepository,
            IMessageQueueService<Article> messageQueueService)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _messageQueueService = messageQueueService ?? throw new ArgumentNullException(nameof(messageQueueService));
        }

        public async Task<TranslateArticleCommandResponse> Handle(TranslateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken);
            
            var messageSentSuccess = await _messageQueueService.Publish(article, "translate_article", cancellationToken);
            if (messageSentSuccess)
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
