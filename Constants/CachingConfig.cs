using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multigate.Titanic.MediatR.Shared.Constants
{
    public static class CachingConfig
    {
        public const int ShortTermCacheDurationInMinutes = 5;
        public const int LongTermCacheDurationInMinutes = 30;
    }
}
