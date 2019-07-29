using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lettuce.ORM.ServiceInterfaces
{
    public interface IEntityDataMapping<TEntity>
    {
        /// <summary>
        /// 映射对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        TEntity MapEntity(IDataReader dataReader);
    }
}
