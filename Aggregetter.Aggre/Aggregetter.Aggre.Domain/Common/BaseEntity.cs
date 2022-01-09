using System;
using System.ComponentModel.DataAnnotations;

namespace Aggregetter.Aggre.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
    }
}
