using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
