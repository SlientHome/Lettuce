using Lettuce.ORM.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Lettuce.ORM
{
    public class EntityMapping
    {



        public static List<T> MapReaderToModel<T>(IDataReader reader)
        {

            string[] fileList = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                fileList[i] = reader.GetName(i);
            }
            Func<IDataReader, object> serializeMethod = GenerateSerializer<T>();
            return null;
        }

        public static Func<IDataReader, List<T> > GenerateSerializer<T>()
        {
            return null;
            var type = typeof(T);
            var propertyInfos = type.GetProperties();
            List<PropertyMethodModel> setMethodInfos = new List<PropertyMethodModel>(propertyInfos.Length);
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                if(propertyInfos[i].SetMethod != null)
                {
                    setMethodInfos.Add(new PropertyMethodModel()
                    {
                        PropertyName = propertyInfos[i].Name,
                        SetMethod = propertyInfos[i].SetMethod
                    });
                }
            }

            // read() 方法
            MethodInfo DataReader_Read = typeof(IDataReader).GetMethod("Read");

            //返回类型
            Type returnListType = typeof(List<T>);

            DynamicMethod dymMethod = new DynamicMethod("GetEmitList_PropertyMethod_"+type.Name, returnListType, new Type[] { typeof(IDataReader) }, true);
            //返回类型 构造器
            ConstructorInfo constructorInfo = returnListType.GetConstructors().FirstOrDefault();

            ILGenerator il = dymMethod.GetILGenerator();


            

            // 定义 List<T> 
            il.DeclareLocal(returnListType);
            // new 一个新的 List<T> 
            il.Emit(OpCodes.Newobj, constructorInfo);
            // 存到Record Frame 第0个位置
            il.Emit(OpCodes.Stloc, 0);

            /* while起点  */
            Label whileStartLabel = il.DefineLabel();

            // 把第0个参数 IDataReader 加载到计算栈 
            il.Emit(OpCodes.Ldarg, 0);



            // 调用IDataReader.Read()方法  返回true false 到计算栈
            il.Emit(OpCodes.Callvirt, DataReader_Read);





            il.Emit(OpCodes.Ret);






        }






    }
}
