using Aggregetter.Aggre.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregetter.Aggre.Domain.Links
{
    public sealed class ArticleCategory
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
