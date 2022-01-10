using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsProviderDto
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public GetArticleDetailsLanguageDto Language { get; set; }
    }
}
