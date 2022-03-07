using MediatR;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.Translate
{
    public sealed class TranslateArticleCommand : IRequest<TranslateArticleCommandResponse>
    {
        public int ArticleId { get; set; }
    }
}
