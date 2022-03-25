using MediatR;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.TranslateArticle
{
    public sealed class TranslateArticleCommand : IRequest<TranslateArticleCommandResponse>
    {
        public string ArticleSlug { get; set; }
    }
}
