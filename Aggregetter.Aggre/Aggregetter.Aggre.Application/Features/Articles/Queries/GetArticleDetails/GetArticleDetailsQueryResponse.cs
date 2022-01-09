using Aggregetter.Aggre.Application.Responses;
using System;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryResponse : BaseResponse<ArticleDetailsDto>
    {
        public GetArticleDetailsQueryResponse(ArticleDetailsDto article) : base(article) { }
    }
}
