using System.Net;

namespace MediatR.Shared.Entities
{
    public abstract class CqrsResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ErrorMessage { get; set; }
    }
}
