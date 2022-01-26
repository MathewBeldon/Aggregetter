using System;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed record GetArticleDetailsDto
    {
        public string TranslatedTitle { get; init; }
        public string OriginalTitle { get; init; }
        public string TranslatedBody { get; init; }
        public string OriginalBody { get; init; }
        public string Endpoint { get; init; }
        public string ArticleSlug { get; init; }
        public DateTime CreatedDateUtc { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime ModifiedDateUtc { get; init; }
        public GetArticleDetailsProviderDto Provider { get; init; }
        public GetArticleDetailsCategoryDto Category { get; init; }
    }
}
