using Lettuce.ORM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Text;

namespace Lettuce.ORM
{
    public static class ILMapHelper
    {

        /// <summary>
        /// 使用时 栈顶必须是Entity对象
        /// </summary>
        /// <param name="il"></param>
        /// <param name="fieldEntityInfo"></param>
        /// <param name="dataReaderIndex"></param>
        public static void GetFieldValueToStackTop(ILGenerator il, FieldEntityInfo fieldEntityInfo,int dataReaderIndex)
        {
            // 加载datareader进来
            il.Emit(OpCodes.Ldarg,0);
            // 数据位置
            il.Emit(OpCodes.Ldc_I4, dataReaderIndex);
            il.Emit(OpCodes.Callvirt, ILCallFunction.DataReaderGetValueMethodInfo);

            if ( fieldEntityInfo.FieldInDbType == typeof(string) )
            {
                il.Emit(OpCodes.Callvirt, ILCallFunction.ConvertToStringMethodInfo);
            }else if (fieldEntityInfo.FieldInDbType == typeof(int))
            {
                il.Emit(OpCodes.Callvirt, ILCallFunction.ConvertToInt32MethodInfo);
            }
           
        }
    }
}
