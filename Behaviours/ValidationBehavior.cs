using MediatR;
using Microsoft.Extensions.Logging;
using Multigate.Titanic.MediatR.Shared.Entities;
using Multigate.Titanic.MediatR.Shared.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace Multigate.Titanic.MediatR.Shared.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CqrsResponse, new()
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IValidationHandler<TRequest> _validationHandler;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IValidationHandler<TRequest> validationHandler)
        {
            _logger = logger;
            _validationHandler = validationHandler;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            if (_validationHandler is null)
            {
                _logger.LogInformation("{Request} does not have a validation handler configured", requestName);
                return await next();
            }

            var result = await _validationHandler.Validate(request);
            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Validation failed for {Request}. Error: {Error}", requestName, result.Error);
                return new TResponse { StatusCode = System.Net.HttpStatusCode.BadRequest, ErrorMessage = result.Error };
            }

            _logger.LogInformation("Validation Succeeded for {Request}", requestName);
            return await next();
        }
    }
}
