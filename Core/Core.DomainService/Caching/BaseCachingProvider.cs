using Microsoft.Extensions.Caching.Memory;
using System;

namespace Core.DomainService.Caching
{
    public abstract class BaseCachingProvider
    {

        #region Properties

        protected MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        private static readonly object _lock = new object();

        #endregion /Properties

        #region Constructors

        public BaseCachingProvider()
        {
        }

        #endregion /Constructors

        #region Methods

        protected object Get(string key)
        {
            lock (_lock)
            {
                return this._cache.Get(key);
            }
        }

        protected void Add(string key, object value, DateTimeOffset expiration)
        {
            lock (_lock)
            {
                this._cache.Set(key, value, expiration);
            }
        }

        protected void Remove(string key)
        {
            lock (_lock)
            {
                this._cache.Remove(key);
            }
        }

        #endregion /Methods

    }
}
