using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Aggregetter.Aggre.Domain.Common
{
    [Index(nameof(Id))]
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ModifiedDateUtc { get; set; }

        //[Timestamp]
        //public byte[] ConcurrencyToken { get; set; }
    }
}
