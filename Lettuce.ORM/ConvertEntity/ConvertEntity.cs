using Lettuce.ORM.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Lettuce.ORM.ConvertEntity
{
    internal class ConvertEntity<TEntity> where TEntity:class
    {


        #region Type & MethodInfo
        internal readonly static Type DbNullType = typeof(DBNull);
        internal readonly static MethodInfo GetTypeMethodInfo = typeof(object).GetMethod("GetType");
        internal readonly static MethodInfo ToStringMethodInfo = typeof(object).GetMethod("ToString");
        #endregion

        internal static ConcurrentDictionary<string, ConvertEntity<TEntity>> Cache = new ConcurrentDictionary<string, ConvertEntity<TEntity>>();
        public readonly static MethodInfo DataReaderGetValueMethodInfo = typeof(IDataRecord).GetMethod("GetValue");
        /// <summary>
        /// 识别用的key
        /// </summary>
        public readonly string IdentityKey;
        /// <summary>
        /// 字段
        /// </summary>
        public readonly List<FieldEntityInfo> ExistFieldsList;

        private Func<IDataReader, TEntity> ConvertFunc { get; set; } = null;

        /// <summary>
        /// 生成数据库读取方法
        /// </summary>
        /// <returns></returns>
        public Func<IDataReader, TEntity> GenerateEntityMapperFunc()
        {
            var type = typeof(TEntity);

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
            // 存object用的局部变量
            var getValueTempObject = il.DeclareLocal(typeof(object));
            var dbNullTypeObj = il.DeclareLocal(typeof(Type));
            il.Emit(OpCodes.Ldtoken, DbNullType);
            il.Emit(OpCodes.Stloc, dbNullTypeObj);
            for (int i = 0; i < ExistFieldsList.Count; i++)
            {
                Label ifContent = il.DefineLabel();
                Label ifStart = il.DefineLabel();
                Label ifOut = il.DefineLabel();
                // 用于接收读出来的数据
                var getValueFromReader = il.DeclareLocal(ExistFieldsList[i].FieldInDbType);
                il.Emit(OpCodes.Ldarg, 0);
                // 数据位置
                il.Emit(OpCodes.Ldc_I4, ExistFieldsList[i].Index );
                // 调用datareader.Getvalue()  返回值到栈顶
                il.Emit(OpCodes.Callvirt, DataReaderGetValueMethodInfo);
                il.Emit(OpCodes.Stloc, getValueTempObject);
               
                il.Emit(OpCodes.Br_S, ifStart);
                // 外层 IF 中的内容
                il.MarkLabel(ifContent);
                // 如果实体是string类型  用ToString() 处理
                if(ExistFieldsList[i].FieldInEntityModelType == typeof(string))
                {
                    // 读取创建的对象
                    il.Emit(OpCodes.Ldloc, entityIL);
                    // 读取值
                    il.Emit(OpCodes.Ldloc, getValueTempObject);
                    il.Emit(OpCodes.Callvirt, ToStringMethodInfo);
                    // set 值
                    il.Emit(OpCodes.Callvirt, ExistFieldsList[i].SetMethod);
                }
                else
                {
                    il.Emit(OpCodes.Ldloc, getValueTempObject);
                    // 把栈顶的值 拆箱成FieldInDbType类型
                    il.Emit(OpCodes.Unbox_Any, ExistFieldsList[i].FieldInDbType);
                    // 保存
                    il.Emit(OpCodes.Stloc, getValueFromReader);
                    // 读取创建的对象
                    il.Emit(OpCodes.Ldloc, entityIL);
                    // 读取值
                    il.Emit(OpCodes.Ldloc, getValueFromReader);
                    // set 值
                    il.Emit(OpCodes.Callvirt, ExistFieldsList[i].SetMethod);
                }
                il.Emit(OpCodes.Br_S, ifOut);


                // IF 开始
                il.MarkLabel(ifStart);
                il.Emit(OpCodes.Ldloc, getValueTempObject);
                il.Emit(OpCodes.Call, GetTypeMethodInfo);
                il.Emit(OpCodes.Ldloc, dbNullTypeObj);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Brfalse, ifContent);
                // 退出 if
                il.MarkLabel(ifOut);

            }
            // 读取对象
            il.Emit(OpCodes.Ldloc, entityIL);
            // 返回
            il.Emit(OpCodes.Ret);
            /*
             C# 代码
             TEntity function (IDataReader reader){
                    TEntity entity = new TEntity();
                    object tempValue;
                    Type dbNullType = typeof(System.DbNull);
                    
                    tempValue = reader.GetValue(1);
                     if(tempValue.GetType() != dbNullType){
                            
                            entity.set_xxx((FiledType)tempValue);
                     }
                     .....
                     每一列
                     .....
                     // 如果实体是string类型来接收
                     tempValue = reader.GetValue(1);
                     if(tempValue.GetType() != dbNullType){
                            entity.set_xxx(tempValue.ToString());
                     }
                   return entity;
            }
             
             */
            Func<IDataReader, TEntity> function = (Func<IDataReader, TEntity>)dymMethod.CreateDelegate(typeof(Func<IDataReader, TEntity>));
            ConvertFunc = function;
            return function;
        }
        /// <summary>
        /// 获得映射Func
        /// </summary>
        /// <returns></returns>
        public Func<IDataReader, TEntity> GetConvertFunc()
        {
            if(ConvertFunc == null)
            {
                GenerateEntityMapperFunc();
            }
            return ConvertFunc;
        }
        public ConvertEntity(List<FieldEntityInfo> fieldEntityInfos)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeof(TEntity).Name).Append("_");
            fieldEntityInfos.ForEach(t => sb.Append(t.FieldName.ToLower()));
            IdentityKey =  sb.ToString();
            ExistFieldsList = fieldEntityInfos;
        }

    }
}
