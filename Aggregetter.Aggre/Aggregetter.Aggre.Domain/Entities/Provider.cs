using Aggregetter.Aggre.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    public sealed class Provider : BaseEntity
    {
        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [Column(TypeName = "tinytext")]
        public string Name { get; set; }

        [Column(TypeName = "tinytext")]
        public string BaseAddress { get; set; }
    }
}
