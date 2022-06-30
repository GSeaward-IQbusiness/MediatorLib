using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Multigate.Titanic.MediatR.Shared.Caching;
using Multigate.Titanic.MediatR.Shared.Constants;

namespace Multigate.Titanic.MediatR.Shared.Behaviours
{
    public class LongTermCacheBehaviour<TRequest, TResponse> : BaseCacheBehaviour<TRequest, TResponse>
        where TRequest : ILongTermCachable
    {
        public LongTermCacheBehaviour(IMemoryCache cache, ILogger<BaseCacheBehaviour<TRequest, TResponse>> logger)
            : base(cache, logger)
        {
        }

        protected override int GetCacheDuration()
        {
            return CachingConfig.LongTermCacheDurationInMinutes;
        }
    }
}
