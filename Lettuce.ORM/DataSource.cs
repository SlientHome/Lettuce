using Lettuce.ORM.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Lettuce.ORM
{
    public enum DataBaseType
    {
        SqlServer,
        Mysql
    }
    public class DataSource:IDisposable
    {
        private DatasourceConfig _datasourceConfig;
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        public DataSource(DatasourceConfig datasourceConfig)
        {
            _datasourceConfig = datasourceConfig;
            _connection = GetConnection();
        }

        private IDbConnection GetConnection()
        {
            IDbConnection dbConnection = null;
            switch (_datasourceConfig.DbType)
            {
                case DataBaseType.SqlServer:
                    dbConnection = new SqlConnection(_datasourceConfig.ConnectString);
                    break;
                case DataBaseType.Mysql:
                    dbConnection = new MySqlConnection(_datasourceConfig.ConnectString);
                    break;
            }
            return dbConnection;
        }
        public void OpenTransaction()
        {
            IDbTransaction transaction = _connection.BeginTransaction();
            _transaction = transaction;
        }
        public void Commit()
        {
            if(_transaction == null)
            {
                return;
            }
            _transaction.Commit();
        }
        public void Rollback()
        {
            if (_transaction == null)
            {
                return;
            }
            _transaction.Rollback();
        }
        public void Dispose()
        {
            _connection.Close();
        }



        #region Command

         


        #endregion
    }
}
