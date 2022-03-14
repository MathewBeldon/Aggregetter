using MediatR;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.Translate
{
    public sealed class TranslateArticleCommand : IRequest<TranslateArticleCommandResponse>
    {
        public string ArticleSlug { get; set; }
    }
}
