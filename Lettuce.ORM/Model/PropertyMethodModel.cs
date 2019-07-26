using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM.Model
{
    class PropertyMethodModel
    {
        public MethodInfo SetMethod{get;set;}
        public string PropertyName { get; set; }
    }

}
