using Aggregetter.Aggre.Application.Models.Base;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryResponse : BaseResponse<GetArticleDetailsDto>
    {
        [JsonConstructor]
        public GetArticleDetailsQueryResponse(GetArticleDetailsDto article = default(GetArticleDetailsDto)) : base(article) { }
    }
}
