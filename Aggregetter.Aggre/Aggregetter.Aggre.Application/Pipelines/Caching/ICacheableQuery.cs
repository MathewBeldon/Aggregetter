using System;

namespace Aggregetter.Aggre.Application.Pipelines.Caching
{
    public interface ICacheableQuery
    {
        string Key { get; }
        bool Bypass { get; }
        TimeSpan? AbsoluteExpiration { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}
