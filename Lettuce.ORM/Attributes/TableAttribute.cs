using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM.Attributes
{
    public class TableAttribute: Attribute
    {
        public string TableName { get; set; }
        public TableAttribute(string TableName)
        {
            this.TableName = TableName;
        }

    }
}
