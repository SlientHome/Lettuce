using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM.Attributes
{
    public class PrimaryKeyAttribute: Attribute
    {
        /// <summary>
        /// 是否是由数据库生成
        /// </summary>
        public bool IsGenerateKey { get; set; }

        public PrimaryKeyAttribute(bool IsGenerateKey = false)
        {
            this.IsGenerateKey = IsGenerateKey;
        }


    }
}
