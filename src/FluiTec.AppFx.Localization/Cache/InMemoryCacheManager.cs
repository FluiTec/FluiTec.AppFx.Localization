using System.Collections.Concurrent;
using FluiTec.DbLocalizationProvider.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace FluiTec.AppFx.Localization.Cache
{
    /// <summary>   Manager for in memory caches. </summary>
    public class InMemoryCacheManager : ICacheManager
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="memCache"> The memory cache. </param>
        public InMemoryCacheManager(IMemoryCache memCache)
        {
            _memCache = memCache;
        }

        #endregion

        #region Fields

        /// <summary>   The entries. </summary>
        internal static readonly ConcurrentDictionary<string, bool> Entries = new ConcurrentDictionary<string, bool>();

        /// <summary>   The memory cache. </summary>
        private readonly IMemoryCache _memCache;

        #endregion

        #region Methods

        /// <summary>   You should add given value to the cache under given key. </summary>
        /// <param name="key">      Key identifier of the cached item. </param>
        /// <param name="value">    Actual value fo the cached item. </param>
        public void Insert(string key, object value)
        {
            _memCache.Set(key, value);
            Entries.TryAdd(key, true);
        }

        /// <summary>
        ///     You should implement this method to get cached item back from the underlying storage.
        /// </summary>
        /// <param name="key">  Key identifier of the cached item. </param>
        /// <returns>   Actual value fo the cached item. Take care of casting back to proper type. </returns>
        public object Get(string key)
        {
            return _memCache.Get(key);
        }

        /// <summary>
        ///     If you want to remove the cached item from the storage - this is the meethod to implement
        ///     then.
        /// </summary>
        /// <param name="key">  Key identifier of the cached item. </param>
        public void Remove(string key)
        {
            _memCache.Remove(key);
            Entries.TryRemove(key, out _);
        }

        #endregion

        #region Events

        /// <summary>
        ///     Event raise is taken care by
        ///     <see cref="T:DbLocalizationProvider.Cache.BaseCacheManager" />.
        /// </summary>
        public event CacheEventHandler OnInsert;

        /// <summary>
        ///     Event raise is taken care by
        ///     <see cref="T:DbLocalizationProvider.Cache.BaseCacheManager" />.
        /// </summary>
        public event CacheEventHandler OnRemove;

        #endregion
    }
}