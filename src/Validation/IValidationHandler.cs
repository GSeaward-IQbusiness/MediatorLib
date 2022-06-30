using System.Threading.Tasks;

namespace MediatR.Shared.Validation
{
    public interface IValidationHandler { }
    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
