using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM
{
    public class LettuceSqlCommand : ISqlCommand
    {
        //internal static ConcurrentDictionary<string, ConvertEntity<TEntity>> Cache = new ConcurrentDictionary<string, ConvertEntity<TEntity>>();
        public int Delete<T>(T entity)
        {
            TypeInfoCache.GetTypeInfo<T>();


            throw new NotImplementedException();
        }

        public int DeleteList<T>(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public int InsertList<T>(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateList<T>(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
