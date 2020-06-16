using System.Collections.Concurrent;

namespace FluiTec.DbLocalizationProvider.Cache
{
    public class InMemoryCache : ICacheManager
    {
        private static readonly ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();

        public void Insert(string key, object value)
        {
            Cache.TryAdd(key, value);
        }

        public object Get(string key)
        {
            return Cache.GetOrAdd(key, k => null);
        }

        public void Remove(string key)
        {
            Cache.TryRemove(key, out _);
        }

        public event CacheEventHandler OnInsert;
        public event CacheEventHandler OnRemove;
    }
}