namespace MediatR.Shared.Entities
{
    public class CommandResponse<TValue> : CqrsResponse
    {
        public TValue Value { get; set; }
    }
}
