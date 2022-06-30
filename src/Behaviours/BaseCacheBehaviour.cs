using MediatR;
using MediatR.Shared.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Shared.Behaviours
{
    public abstract class BaseCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICachable
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

            if (response != null)
                _cache.Set(request.CacheKey, response, TimeSpan.FromMinutes(GetCacheDuration()));
            return response;
        }
    }
}
