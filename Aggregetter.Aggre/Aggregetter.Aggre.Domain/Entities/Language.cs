﻿using Aggregetter.Aggre.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Entities
{
    public sealed class Language : BaseEntity
    {
        [Column(TypeName = "tinytext")]
        public string Name { get; set; }
    }
}
