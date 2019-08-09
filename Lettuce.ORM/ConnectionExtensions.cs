using Lettuce.ORM.ConvertEntity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Lettuce.ORM
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ConnectionExtensions
    {

        

        public static ISqlCommand Command(this IDbConnection connection)
        {
            return new LettuceSqlCommand();
        }
        public static ISqlCommand Command<TCommandClass>(this IDbConnection connection) where TCommandClass : ISqlCommand,new()
        {
            return new TCommandClass();
        }
        public static ISqlCommand Command<TCommandClass>(this IDbConnection connection, TCommandClass commandClass) where TCommandClass : ISqlCommand, new()
        {
            return commandClass;
        }



        public static List<TEntity> FindList<TEntity>(this IDbConnection connection, string sql) where TEntity : class
        {

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            List<TEntity> list = new List<TEntity>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                IDataReader dataReader = command.ExecuteReader();
                var convertFunc = MapHelper.GetConvertFunction<TEntity>(dataReader);

                while (dataReader.Read())
                {
                    var entity = convertFunc(dataReader);
                    list.Add(entity);
                }
                dataReader.Dispose();
            }
            return list;
        }

        public static TEntity FirstOrDefault<TEntity>(this IDbConnection connection, string sql) where TEntity : class
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            TEntity entity = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                IDataReader dataReader = command.ExecuteReader();
                var convertFunc = MapHelper.GetConvertFunction<TEntity>(dataReader);
                while (dataReader.Read())
                {
                    entity = convertFunc(dataReader);
                    break;
                }
                dataReader.Dispose();
            }
            return entity;
        }



    }
}
