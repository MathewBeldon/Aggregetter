using Aggregetter.Aggre.Application.Responses;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandResponse : BaseResponse<CreateArticleDto>
    {
        public CreateArticleCommandResponse(CreateArticleDto data) : base(data) { }
    }
}
