using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lettuce.ORM
{
    public static class ConnectionExtensions
    {





        public static List<TEntity> QuerySql<TEntity>(this IDbConnection connection,string sql) where TEntity:class
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var command = connection.CreateCommand();
            command.CommandText = sql;
            IDataReader dataReader = command.ExecuteReader();

            var convertFunc = MapHelper.GetConvertFunction<TEntity>(dataReader);
            List<TEntity> list = new List<TEntity>();
            while (dataReader.Read())
            {
                var  entity = convertFunc(dataReader);
                list.Add(entity);
            }
            connection.Close();
            return list;
        }



    }
}
