using Aggregetter.Aggre.Application.Models.Base;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryResponse : ContentResponse<GetArticleDetailsDto>
    {
        public GetArticleDetailsQueryResponse() : base() { }
        public GetArticleDetailsQueryResponse(GetArticleDetailsDto article) : base(article) { }
    }
}
