using MediatR;
using MediatR.Shared.Caching;
using MediatR.Shared.Constants;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MediatR.Shared.Behaviours
{
    public class ShortTermCacheBahaviour<TRequest, TResponse> : BaseCacheBehaviour<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IShortTermCachable
    {
        public ShortTermCacheBahaviour(IMemoryCache cache, ILogger<BaseCacheBehaviour<TRequest, TResponse>> logger)
            : base(cache, logger)
        {
        }

        protected override int GetCacheDuration()
        {
            return CachingConfig.ShortTermCacheDurationInMinutes;
        }
    }
}
