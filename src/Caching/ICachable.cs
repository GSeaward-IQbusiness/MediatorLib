namespace MediatR.Shared.Caching
{
    public interface ICachable
    {
        string CacheKey { get; }
    }
}
