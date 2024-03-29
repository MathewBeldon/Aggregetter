﻿using Aggregetter.Aggre.Application.Contracts.Mediator.Transactions;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommand : IWriteRequest<CreateArticleCommandResponse>
    {
        public int ProviderId { get; set; }
        public int CategoryId { get; set; }
        public string TranslatedTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string TranslatedBody { get; set; }
        public string OriginalBody { get; set; }
        public string TranslatedBy { get; set; }
        public string Endpoint { get; set; }
        public string ArticleSlug { get; set; }

        public override string ToString()
        {
            return $@"ProviderId: {ProviderId},
CategoryId: {CategoryId},
TranslatedTitle: {TranslatedTitle},
OriginalTitle: {OriginalTitle},
TranslatedBody: {TranslatedBody},
OriginalBody: {OriginalBody},
TranslatedBy: {TranslatedBy},
Endpoint: {Endpoint},
ArticleSlug: {ArticleSlug}";
        }
    }
}
