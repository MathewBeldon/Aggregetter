using Aggregetter.Aggre.Application.Responses;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryResponse : BaseResponse<GetArticleDetailsDto>
    {
        public GetArticleDetailsQueryResponse(GetArticleDetailsDto article) : base(article) { }
    }
}
