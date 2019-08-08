using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM
{
    /// <summary>
    /// 缓存 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class CacheStore<TKey, TValue>
    {
        
        internal ConcurrentDictionary<TKey, TValue> _store = new ConcurrentDictionary<TKey, TValue>();
        public void Save(TKey key, TValue value)
        {
            _store.AddOrUpdate(key, value, (k, oldVal) => value);
        }

        public TValue Get(TKey key)
        {
            if (_store.TryGetValue(key, out TValue val))
            {
                return val;
            }
            return default(TValue);
        }

    }
}
