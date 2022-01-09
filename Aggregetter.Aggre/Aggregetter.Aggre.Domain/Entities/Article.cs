using Aggregetter.Aggre.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    public sealed class Article : BaseEntity
    {
        [ForeignKey(nameof(Provider))]
        public int ProviderId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Column(TypeName = "tinytext")]
        public string TranslatedTitle { get; set; }

        [Column(TypeName = "tinytext")]
        public string OriginalTitle { get; set; }

        [Column(TypeName = "text")]
        public string TranslatedBody { get; set; }

        [Column(TypeName = "text")]
        public string OriginalBody { get; set; }
        
        [Column(TypeName = "text")]
        public string Endpoint { get; set; }

        [Column(TypeName = "tinytext")]
        public string ArticleSlug { get; set; }
    }
}
