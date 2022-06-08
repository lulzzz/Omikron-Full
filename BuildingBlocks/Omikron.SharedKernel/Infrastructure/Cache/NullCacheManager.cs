using System;
using System.Collections.Generic;
using System.Text;

namespace Omikron.SharedKernel.Infrastructure.Cache
{
    public class NullCacheManager : ICacheManager
    {
        public void Set<TEntry>(string key, TEntry @object)
        {
        }

        public void Set<TEntry>(string key, TEntry @object, CacheExpirationTime expired)
        {
        }

        public TEntry Get<TEntry>(string key)
        {
            return default;
        }

        public void Remove(string key)
        {
        }

        public bool Exists(string key)
        {
            return default;
        }
    }
}
