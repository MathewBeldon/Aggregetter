using Aggregetter.Aggre.Application.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Pipelines.Caching
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly CacheSettings _settings;
        public CachingPipelineBehavior(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            if (request.Bypass) return await next();
            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();

                var absoluteExpiration = request.AbsoluteExpiration is null ? TimeSpan.FromSeconds(_settings.AbsoluteExpiration) : request.AbsoluteExpiration;
                var slidingExpiration = request.SlidingExpiration is null ? TimeSpan.FromSeconds(_settings.SlidingExpiration) : request.SlidingExpiration;

                var options = new DistributedCacheEntryOptions { 
                    AbsoluteExpirationRelativeToNow = absoluteExpiration,
                    SlidingExpiration = slidingExpiration
                };

                var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));

                await _cache.SetAsync(request.Key, serializedData, options, cancellationToken);

                return response;
            }
            var cachedResponse = await _cache.GetAsync(request.Key, cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
                _logger.LogInformation($"Fetched from Cache -> '{request.Key}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation($"Added to Cache -> '{request.Key}'.");
            }
            return response;
        }
    }
}
