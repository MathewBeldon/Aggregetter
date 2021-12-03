using Aggregetter.Aggre.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    public sealed class Language : BaseEntity
    {
        [Key]
        public Guid LanguageId { get; set; }

        [Column(TypeName = "tinytext")]
        public string Name { get; set; }
    }
}
