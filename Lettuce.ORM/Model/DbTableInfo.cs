using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM.Model
{

    public class DbFieldInfo
    {
        public PropertyInfo Property { get; set; }
        public string FieldInDbName { get; set; }
    }

    public class DbTableInfo
    {
        public string TableName { get; set; }
        public Type Type { get; set; }
        public List<DbFieldInfo> FieldPropertyInfos { get; set; } = new List<DbFieldInfo>();
        public DbFieldInfo PrimaryKeyField { get; set; }

    }
}
