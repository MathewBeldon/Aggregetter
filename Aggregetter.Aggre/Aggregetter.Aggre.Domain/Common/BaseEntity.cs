using System;

namespace Aggregetter.Aggre.Domain.Common
{
    public class BaseEntity
    {
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
    }
}
