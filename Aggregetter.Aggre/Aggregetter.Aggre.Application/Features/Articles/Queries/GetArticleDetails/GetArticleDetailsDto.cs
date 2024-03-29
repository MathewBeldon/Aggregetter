﻿using System;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed record GetArticleDetailsDto(
        string TranslatedTitle, 
        string OriginalTitle,
        string TranslatedBody,
        string OriginalBody,
        string Endpoint,
        string ArticleSlug,
        DateTimeOffset CreatedDateUtc,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] DateTimeOffset ModifiedDateUtc,
        GetArticleDetailsProviderDto Provider,
        GetArticleDetailsCategoryDto Category
    );
}
