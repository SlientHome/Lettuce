using Lettuce.ORM.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Lettuce.ORM
{
    public class EntityDataMapping<TEntity> where TEntity:class
    {
        public readonly static MethodInfo DataReaderGetValueMethodInfo = typeof(IDataRecord).GetMethod("GetValue");
        /// <summary>
        /// 识别用的key
        /// </summary>
        public readonly string IdentityKey;
        /// <summary>
        /// 字段
        /// </summary>
        public readonly List<FieldEntityInfo> ExistFieldsList;

        private Func<IDataReader, TEntity> MapFunc { get; set; } = null;
        private const int ERROR_LIMIT = 2;
        /// <summary>
        /// 生成数据库读取方法
        /// </summary>
        /// <returns></returns>
        public Func<IDataReader, TEntity> GenerateEntityMapperFunc()
        {
            var type = typeof(TEntity);
            var propertyInfos = type.GetProperties().Where(t=> t.GetSetMethod()!=null).ToList();
            List<FieldEntityInfo> unionFiled = new List<FieldEntityInfo>();

            for(int i = 0; i < ExistFieldsList.Count; i++)
            {
                if(propertyInfos.Exists(t => t.Name.ToLower() == ExistFieldsList[i].FieldName.ToLower()))
                {
                    unionFiled.Add(ExistFieldsList[i]);
                }
            }

            DynamicMethod dymMethod = new DynamicMethod("GetEntity_PropertyMethod_"+type.Name, type, new Type[] { typeof(IDataReader) }, true);
            // 对象 默认无参构造函数
            ConstructorInfo entityConstructorInfo = typeof(TEntity).GetConstructors().FirstOrDefault(t => t.GetParameters().Length == 0);
            if (entityConstructorInfo == null)
            {
                throw new Exception("无参构造器不存在");
            }
            ILGenerator il = dymMethod.GetILGenerator();
            // 实例化对象
            var entityIL = il.DeclareLocal(typeof(TEntity));// 局部变量位置 0
            il.Emit(OpCodes.Newobj, entityConstructorInfo);
            // 保存起来
            il.Emit(OpCodes.Stloc, entityIL);

            for (int i = 0; i < unionFiled.Count; i++)
            {
                // 用于接收读出来的数据
                var getValueFromReader = il.DeclareLocal(unionFiled[i].FieldInDbType);
                il.Emit(OpCodes.Ldarg, 0);
                // 数据位置
                il.Emit(OpCodes.Ldc_I4, unionFiled[i].Index );
                // 调用datareader.Getvalue()  返回值到栈顶
                il.Emit(OpCodes.Callvirt, DataReaderGetValueMethodInfo);
                // 把栈顶的值 拆箱成FieldInDbType类型
                il.Emit(OpCodes.Unbox_Any, unionFiled[i].FieldInDbType);
                // 保存
                il.Emit(OpCodes.Stloc, getValueFromReader);
                // 读取创建的对象
                il.Emit(OpCodes.Ldloc, entityIL);
                // 读取值
                il.Emit(OpCodes.Ldloc, getValueFromReader);
                // set 值
                il.Emit(OpCodes.Callvirt, unionFiled[i].SetMethod);
            }
            // 读取对象
            il.Emit(OpCodes.Ldloc, entityIL);
            // 返回
            il.Emit(OpCodes.Ret);

            Func<IDataReader, TEntity> function = (Func<IDataReader, TEntity>)dymMethod.CreateDelegate(typeof(Func<IDataReader, TEntity>));

            return function;
        }
        /// <summary>
        /// 映射对象
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public TEntity MapEntity(IDataReader dataReader)
        {
            TEntity entity = null;
            if (MapFunc == null)
            {
                MapFunc = GenerateEntityMapperFunc();
            }
            int errorCount = 0;
            while (errorCount < ERROR_LIMIT)
            {
                try
                {
                    if (errorCount != 0)
                    {
                        MapFunc = GenerateEntityMapperFunc();
                    }
                    entity = MapFunc(dataReader);
                    break;
                }
                catch(Exception ex)
                {
                    errorCount++;
                    // TODO
                }
            }
            return entity;
        }

        public EntityDataMapping(List<FieldEntityInfo> fieldEntityInfos)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeof(TEntity).Name).Append("_");
            fieldEntityInfos.ForEach(t => sb.Append(t.FieldName.ToLower()));
            IdentityKey =  sb.ToString();
            ExistFieldsList = fieldEntityInfos;
        }

    }
}
