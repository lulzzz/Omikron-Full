namespace Omikron.SharedKernel.Infrastructure.Cache
{
    public interface ICacheManager
    {
        void Set<TEntry>(string key, TEntry @object);
        void Set<TEntry>(string key, TEntry @object, CacheExpirationTime expired);
        TEntry Get<TEntry>(string key);
        void Remove(string key);
        bool Exists(string key);
    }
}