using Aggregetter.Aggre.Application.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Pipelines.Caching
{
    public sealed class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _settings;
        public CachingPipelineBehavior(IDistributedCache cache, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            if (request.Bypass) return await next();
            
            var cachedResponse = await _cache.GetAsync(request.Key, cancellationToken);
            if (cachedResponse is not null)
            {
                return JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));                
            }

            return await GetResponseAndAddToCacheAsync();

            async Task<TResponse> GetResponseAndAddToCacheAsync()
            {
                response = await next();

                var absoluteExpiration = request.AbsoluteExpiration is null ? TimeSpan.FromSeconds(_settings.AbsoluteExpiration) : request.AbsoluteExpiration;
                var slidingExpiration = request.SlidingExpiration is null ? TimeSpan.FromSeconds(_settings.SlidingExpiration) : request.SlidingExpiration;

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpiration,
                    SlidingExpiration = slidingExpiration
                };

                var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));

                _ = Task.Run(() => _cache.SetAsync(request.Key, serializedData, options, cancellationToken));

                return response;
            }
        }
    }
}
