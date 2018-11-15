using System;

namespace Net.Lab.Liashkevich.Redis.Caching.Interfaces
{
    public interface IRedisCacheProvider : IDisposable
    {
        bool Set<T>(string key, T data);

        T Get<T>(string key);

        bool Remove(string key);

        bool IsInCache(string key);
    }
}
