using Aggregetter.Aggre.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Column(TypeName = "tinytext")]
        public string Name { get; set; }
    }
}
