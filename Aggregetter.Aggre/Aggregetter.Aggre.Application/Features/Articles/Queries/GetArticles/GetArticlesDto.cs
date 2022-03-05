using System;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed record GetArticlesDto(
        string ArticleSlug,
        string TranslatedTitle,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] DateTime TranslatedDateUtc,
        string Endpoint,
        GetArticlesCategoryDto Category,
        GetArticlesProviderDto Provider
    );
}
