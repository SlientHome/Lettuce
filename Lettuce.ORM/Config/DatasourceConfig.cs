using Lettuce.ORM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM.Config
{
    public class DatasourceConfig
    {
        public string ConnectString { get; set; }
        public DataBaseType DbType { get; set; } = DataBaseType.SqlServer;
    }
}
