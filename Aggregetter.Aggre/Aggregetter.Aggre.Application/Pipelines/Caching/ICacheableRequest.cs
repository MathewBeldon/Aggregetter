using System;

namespace Aggregetter.Aggre.Application.Pipelines.Caching
{
    public interface ICacheableRequest
    {
        string Key { get; }
        bool Bypass { get; }
        TimeSpan? AbsoluteExpiration { get; }
    }
}
