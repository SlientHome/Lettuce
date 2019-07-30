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


        #region ConvertFunction
        public static MethodInfo ConvertToInt32MethodInfo { get; private set; } = typeof(ILCallFunction).GetMethod("ConvertToInt32");
        public static MethodInfo ConvertToStringMethodInfo { get; private set; } = typeof(ILCallFunction).GetMethod("ConvertToString");

        public static string ConvertToString(object value)
        {
            return value.ToString();
        }
        public static int ConvertToInt32(object value)
        {
            return Convert.ToInt32(value);
        }


        #endregion


    }
}
