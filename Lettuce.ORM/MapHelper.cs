using Lettuce.ORM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM
{
    public class MapHelper
    {
        public static string GenerateIdentityKey<TEntity>(IDataReader reader)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeof(TEntity).Name).Append("_");
            for(int i = 0; i < reader.FieldCount; i++)
            {
                sb.Append(reader.GetName(i).ToLower());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得转换对象的方法
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Func<IDataReader, TEntity> GetConvertFunction<TEntity>(IDataReader reader) where TEntity:class
        {
            string key = GenerateIdentityKey<TEntity>(reader);
            if(ConvertEntity<TEntity>.Cache.TryGetValue(key, out ConvertEntity<TEntity> val))
            {
               return  val.GetConvertFunc();
            }
            else
            {
                var fieldInfoList = new List<Model.FieldEntityInfo>();
                var type = typeof(TEntity);

                var propertyInfos = type.GetProperties().Where(t => t.GetSetMethod() != null).ToList();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    // TODO 严格匹配
                    string fieldName = reader.GetName(i).ToLower();
                    PropertyInfo property = propertyInfos.FirstOrDefault(t=>t.Name.ToLower() == fieldName);

                    if (property == null)
                        continue;

                    fieldInfoList.Add(new FieldEntityInfo()
                    {
                        Index = i,
                        SetMethod = property.SetMethod,
                        FieldName = fieldName,
                        FieldInDbType = reader.GetFieldType(i)
                    });

                }

                ConvertEntity<TEntity> convertEntity = new ConvertEntity<TEntity>(fieldInfoList);

                if(ConvertEntity<TEntity>.Cache.TryAdd(key, convertEntity))
                {
                    return convertEntity.GenerateEntityMapperFunc();
                }
                else
                {
                    throw new Exception("新增ConvertEntity失败");
                }

            }
        }
    }
}
