using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM.Attributes
{
    public class KeyAttribute: Attribute
    {
        public string Name { get; set; }
        public KeyAttribute(string name)
        {
            Name = name;
        }


    }
}
