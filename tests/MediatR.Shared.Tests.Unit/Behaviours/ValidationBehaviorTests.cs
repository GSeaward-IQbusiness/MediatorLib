using FluentAssertions;
using MediatR.Shared.Behaviours;
using MediatR.Shared.Entities;
using MediatR.Shared.Validation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MediatR.Shared.Tests.Unit.Behaviours
{
    public class ValidationBehaviourNullHandlerTests
    {
        private readonly ValidationBehavior<IRequest<TestResponse>, TestResponse> _sut;

        private readonly ILogger<ValidationBehavior<IRequest<TestResponse>, TestResponse>> _logger = Substitute.For<ILogger<ValidationBehavior<IRequest<TestResponse>, TestResponse>>>();

        public ValidationBehaviourNullHandlerTests()
        {
            _sut = new ValidationBehavior<IRequest<TestResponse>, TestResponse>(_logger);
        }

        [Fact]
        public async Task Handle_ShouldInvokeNextDelegate_WhenHandlerIsNull()
        {
            // Arrange
            var testResponse = new TestResponse();
            var next = new RequestHandlerDelegate<TestResponse>(() => Task.FromResult(testResponse));
            var request = new TestRequest();
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _sut.Handle(request, cancellationTokenSource.Token, next);

            // Assert
            var requestName = request.GetType();
            // _logger.Received().Log(LogLevel.Information,"{Request} does not have a validation handler configured", requestName)
            result.Should().BeEquivalentTo(testResponse);
        }
    }

    public class ValidationBehaviorTests
    {
        private readonly ValidationBehavior<IRequest<TestResponse>,TestResponse> _sut;

        private readonly ILogger<ValidationBehavior<IRequest<TestResponse>, TestResponse>> _logger = Substitute.For<ILogger<ValidationBehavior<IRequest<TestResponse>, TestResponse>>>();
        private readonly IValidationHandler<IRequest<TestResponse>> _validationHandler = Substitute.For<IValidationHandler<IRequest<TestResponse>>>();

        public ValidationBehaviorTests()
        {
            _sut = new ValidationBehavior<IRequest<TestResponse>, TestResponse>(_logger, _validationHandler);
        }

       
    }

    public class TestRequest : IRequest<TestResponse>
    {

    }

    public class TestResponse : CqrsResponse
    {

    }
}
