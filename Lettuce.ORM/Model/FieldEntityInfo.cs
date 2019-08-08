using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM.Model
{
    public class FieldEntityInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段在数据库中的类型
        /// </summary>
        public Type FieldInDbType { get; set; }
        /// <summary>
        /// 字段在实体类中的类型
        /// </summary>
        public Type FieldInEntityModelType { get; set; }
        /// <summary>
        /// 设置字段的Set方法
        /// </summary>
        public MethodInfo SetMethod { get; set; }
        /// <summary>
        /// datareader的 i 
        /// </summary>
        public int Index { get; set; }
    }
}
