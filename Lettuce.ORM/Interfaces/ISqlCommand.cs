using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM
{
    public interface ISqlCommand
    {
        int Insert<T>(T entity);
        int InsertList<T>(IEnumerable<T> entityList);

        int Delete<T>(T entity);
        int DeleteList<T>(IEnumerable<T> entityList);

        int Update<T>(T entity);
        int UpdateList<T>(IEnumerable<T> entityList);






    }
}
