using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Pipelines.Caching
{
    public interface ICacheableQuery
    {
        string Key { get; }
        bool Bypass { get; }
        TimeSpan? AbsoluteExpiration { get; }
        TimeSpan? SlidingExpiration { get; }

    }
}
