using Humanizer;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Shared.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = GetRequestName(request);
            _logger.LogInformation("{request} is starting.", requestName);

            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();

            _logger.LogInformation("{request} has finished in  {requestTime}ms", requestName, timer.ElapsedMilliseconds);
            return response;
        }

        private string GetRequestName(TRequest request)
        {
            return request.GetType().FullName.Humanize();
        }
    }
}
