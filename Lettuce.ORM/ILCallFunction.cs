using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Lettuce.ORM
{
    public static class ILCallFunction
    {

        /// <summary>
        /// System.Data.IDataRecord::GetValue(int32)
        /// </summary>
        public static MethodInfo DataReaderGetValueMethodInfo { get; private set; } = typeof(IDataRecord).GetMethod("GetValue");



    }
}
