﻿using Aggregetter.Aggre.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    [Index(nameof(ArticleSlug))]
    [Index(nameof(Endpoint))]
    [Index(nameof(ProviderId), nameof(CategoryId))]
    [Index(nameof(ProviderId))]
    [Index(nameof(CategoryId))]
    public sealed class Article : BaseEntity
    {
        [ForeignKey(nameof(Provider))]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column(TypeName = "tinytext")]
        public string TranslatedTitle { get; set; }

        [Column(TypeName = "tinytext")]
        public string OriginalTitle { get; set; }

        [Column(TypeName = "text")]
        public string TranslatedBody { get; set; }

        [Column(TypeName = "text")]
        public string OriginalBody { get; set; }    
        
        public string TranslatedBy { get; set; }
        public DateTime? TranslatedDateUtc { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Endpoint { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ArticleSlug { get; set; }
    }
}
