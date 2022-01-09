using System;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class ArticleDetailsDto
    {        
        public string TranslatedTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string TranslatedBody { get; set; }
        public string OriginalBody { get; set; }
        public string Endpoint { get; set; }
        public string ArticleSlug { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime ModifiedDateUtc { get; set; }
        public ProviderDto Provider { get; set; }
        public CategoryDto Category { get; set; }
    }
}
