﻿using Aggregetter.Aggre.Application.Settings;
using Mediator;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Pipelines.Caching
{
    public sealed class CachingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheableRequest
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _settings;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<CachingPipelineBehaviour<TRequest, TResponse>> _logger;

        public CachingPipelineBehaviour(IDistributedCache cache, 
            IOptions<CacheSettings> settings,
            JsonSerializerOptions jsonSerializerOptions,
            ILogger<CachingPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings.Value));
            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        async ValueTask<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            TResponse response;
            if (request.Bypass) return await next(request, cancellationToken);

            var cachedResponse = await _cache.GetAsync(request.Key, cancellationToken);
            if (cachedResponse is not null)
            {
                _logger.LogInformation("Retrieved {key} from cache", request.Key);
                return JsonSerializer.Deserialize<TResponse>(Encoding.UTF8.GetString(cachedResponse), options: _jsonSerializerOptions);
            }

            return await GetResponseAndAddToCacheAsync();

            async Task<TResponse> GetResponseAndAddToCacheAsync()
            {
                response = await next(request, cancellationToken);

                var absoluteExpiration = request.AbsoluteExpiration ?? TimeSpan.FromSeconds(_settings.AbsoluteExpiration);

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpiration
                };

                var serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response, options: _jsonSerializerOptions));

                _logger.LogInformation("Adding {key} to cache", request.Key);
                _ = Task.Run(() => _cache.SetAsync(request.Key, serializedData, options, cancellationToken));

                return response;
            }
        }
    }
}
