using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM.Model
{
    public class FieldEntityInfo
    {
        public string FieldName { get; set; }
        public Type FieldInDbType { get; set; }
        public Type FieldInEntityModelType { get; set; }
        public MethodInfo SetMethod { get; set; }

        public int Index { get; set; }
    }
}
