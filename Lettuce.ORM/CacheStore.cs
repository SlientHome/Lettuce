using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM
{
    /// <summary>
    /// 缓存 基础类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class CacheStore<TKey,TValue>
    {

        public CacheStore(Func<TKey, TValue> generateValueFunc)
        {
            // 生成
            if (generateValueFunc == null)
                throw new ArgumentNullException(nameof(generateValueFunc));

            GenerateValueFunc = generateValueFunc;
        }

        /// <summary>
        ///  生成值的方法
        /// </summary>
        public Func<TKey,TValue> GenerateValueFunc { get; set; }

        ConcurrentDictionary<TKey, TValue> _store = new ConcurrentDictionary<TKey, TValue>();
        public void Save(TKey key,TValue value)
        {
            _store.AddOrUpdate(key, value, (k, oldVal) => value);
        }

        public TValue Get(TKey key)
        {
            if(_store.TryGetValue(key,out TValue val))
            {
                return val;
            }
            TValue generateValue = GenerateValueFunc(key);
            Save(key, generateValue);
            return generateValue;
        }

    }
}
