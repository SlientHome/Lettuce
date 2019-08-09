using Lettuce.ORM.Attributes;
using Lettuce.ORM.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM
{
    public static class DbTypeInfoCache
    {
        private static CacheStore<Type, DbTableInfo> Cache = new CacheStore<Type, DbTableInfo>();
        private static DbTableInfo LoadDbTableInfo(Type type)
        {
            DbTableInfo dbTableInfo = new DbTableInfo();
            TableAttribute tableAttribute = type.GetCustomAttribute<TableAttribute>();

            dbTableInfo.Type = type;
            if (tableAttribute != null && string.IsNullOrEmpty(tableAttribute.TableName) == false)
            {
                dbTableInfo.TableName = tableAttribute.TableName;
            }
            else
            {
                dbTableInfo.TableName = type.Name;
            }

            foreach (PropertyInfo property in dbTableInfo.Type.GetProperties())
            {
                if (property.GetMethod == null || property.SetMethod == null)
                    continue;
                if (!(property.GetMethod.IsPublic && property.SetMethod.IsPublic))
                    continue;

                DbFieldInfo dbFieldInfo = new DbFieldInfo();
                dbFieldInfo.Property = property;
                KeyAttribute keyAttribute = property.GetCustomAttribute<KeyAttribute>();

                if (keyAttribute != null && string.IsNullOrEmpty(keyAttribute.Name) == false)
                {
                    dbFieldInfo.FieldInDbName = keyAttribute.Name;
                }
                else
                {
                    dbFieldInfo.FieldInDbName = property.Name;
                }

                // 还没有主键的时候
                if (dbTableInfo.PrimaryKeyField == null)
                {
                    PrimaryKeyAttribute primaryKeyAttribute = property.GetCustomAttribute<PrimaryKeyAttribute>();
                    if (primaryKeyAttribute != null)
                    {
                        dbTableInfo.PrimaryKeyField = dbFieldInfo;
                    }
                }
                dbTableInfo.FieldPropertyInfos.Add(dbFieldInfo);
            }

            // 未找到主键标识
            if (dbTableInfo.PrimaryKeyField == null)
            {
                // 尝试寻找 id 列
                dbTableInfo.PrimaryKeyField = dbTableInfo.FieldPropertyInfos.FirstOrDefault(t => t.FieldInDbName.ToLower() == "id");
                // 没有就选择第一个属性
                if (dbTableInfo.PrimaryKeyField == null)
                {
                    dbTableInfo.PrimaryKeyField = dbTableInfo.FieldPropertyInfos.FirstOrDefault();
                }
            }
            return dbTableInfo;

        }
        public static DbTableInfo GetTypeInfo(Type type)
        {
            DbTableInfo dbTable = Cache.Get(type);

            if(dbTable == null)
            {
                dbTable = LoadDbTableInfo(type);
            }
            Cache.Save(type,dbTable);
            return dbTable;
        }
        public static DbTableInfo GetTypeInfo<T>()
        {
            return GetTypeInfo(typeof(T));
        }
    }



}
