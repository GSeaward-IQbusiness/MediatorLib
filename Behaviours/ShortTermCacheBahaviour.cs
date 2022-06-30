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
    public class ShortTermCacheBahaviour<TRequest, TResponse> : BaseCacheBehaviour<TRequest, TResponse> 
        where TRequest : IShortTermCachable
    {
        public ShortTermCacheBahaviour( IMemoryCache cache, ILogger<BaseCacheBehaviour<TRequest, TResponse>> logger)
            :base(cache, logger)
        {
        }

        protected override int GetCacheDuration()
        {
            return CachingConfig.ShortTermCacheDurationInMinutes;
        }
    }
}
