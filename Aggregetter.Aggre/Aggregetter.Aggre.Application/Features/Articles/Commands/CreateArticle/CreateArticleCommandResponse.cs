using Aggregetter.Aggre.Application.Models.Base;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandResponse : ContentResponse<CreateArticleDto>
    {
        public CreateArticleCommandResponse() : base () { }
        public CreateArticleCommandResponse(CreateArticleDto data) : base(data) { }
    }
}
