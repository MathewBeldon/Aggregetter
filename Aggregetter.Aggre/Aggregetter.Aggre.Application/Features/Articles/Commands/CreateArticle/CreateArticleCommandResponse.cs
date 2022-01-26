using Aggregetter.Aggre.Application.Models.Base;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandResponse : BaseResponse<CreateArticleDto>
    {
        public CreateArticleCommandResponse() : base () { }
        public CreateArticleCommandResponse(CreateArticleDto data) : base(data) { }
    }
}
