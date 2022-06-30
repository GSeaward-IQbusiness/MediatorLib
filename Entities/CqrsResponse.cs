using System.Net;

namespace Multigate.Titanic.MediatR.Shared.Entities
{
    public abstract class CqrsResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ErrorMessage { get; set; }
    }
}
