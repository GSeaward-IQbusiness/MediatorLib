using System.Threading.Tasks;

namespace Multigate.Titanic.MediatR.Shared.Validation
{
    public interface IValidationHandler { }
    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
