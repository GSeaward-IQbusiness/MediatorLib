using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Multigate.Titanic.MediatR.Shared.Caching;
using Multigate.Titanic.MediatR.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multigate.Titanic.MediatR.Shared.Behaviours
{
    public abstract class BaseCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachable
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<BaseCacheBehaviour<TRequest, TResponse>> _logger;

        protected abstract int GetCacheDuration();
        protected BaseCacheBehaviour(IMemoryCache cache, ILogger<BaseCacheBehaviour<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            _logger.LogInformation("{request} is configured for caching.", requestName);

            if (_cache.TryGetValue(request.CacheKey, out TResponse response))
            {
                _logger.LogInformation("Returning cached value for {request}", requestName);
                return response;
            }

            _logger.LogInformation("{request} cache key {cacheKey} is not in the cache, executing request", requestName, request.CacheKey);
            response = await next();

            if(response != null)
                _cache.Set(request.CacheKey, response, TimeSpan.FromMinutes(GetCacheDuration()));
            return response;
        }
    }
}
