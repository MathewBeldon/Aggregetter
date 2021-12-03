using Aggregetter.Aggre.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    public sealed class Article : BaseEntity
    {
        [Key]
        public Guid ArticleId { get; set; }

        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

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
    }
}
