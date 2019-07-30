using Lettuce.ORM.Model;
using Lettuce.ORM.ServiceInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Lettuce.ORM
{
    public class EntityDataMapping<TEntity> : IEntityDataMapping<TEntity>
    {

        public List<FieldEntityInfo> ExistFieldsList { get; set; } = new List<FieldEntityInfo>();
        private Func<IDataReader, TEntity> MapFunc { get; set; } = null;
        public Func<IDataReader, TEntity> GenerateEntityMapperFunc()
        {
            var type = typeof(TEntity);
            var propertyInfos = type.GetProperties().Where(t=> t.GetSetMethod()!=null).ToList();

            DynamicMethod dymMethod = new DynamicMethod("GetEntity_PropertyMethod_"+type.Name, type, new Type[] { typeof(IDataReader) }, true);
            // 对象 默认无参构造函数
            ConstructorInfo entityConstructorInfo = typeof(TEntity).GetConstructors().FirstOrDefault(t => t.GetParameters().Length == 0); 

            ILGenerator il = dymMethod.GetILGenerator();
            // 实例化对象
            il.DeclareLocal(typeof(TEntity));// 局部变量位置 0
            il.Emit(OpCodes.Newobj, entityConstructorInfo);
            // 保存起来
            il.Emit(OpCodes.Stloc, 0);


            for(int i = 0; i < ExistFieldsList.Count; i++)
            {
                il.Emit(OpCodes.Ldloc, 0);
                // 栈顶有返回数据
                ILMapHelper.GetFieldValueToStackTop(il, ExistFieldsList[i], i);
                il.Emit(OpCodes.Callvirt, ExistFieldsList[i].SetMethod);
            }

            il.Emit(OpCodes.Ldloc, 0);
            il.Emit(OpCodes.Ret);

            Func<IDataReader, TEntity> function = (Func<IDataReader, TEntity>)dymMethod.CreateDelegate(typeof(Func<IDataReader, TEntity>));
            return function;
        }

        public TEntity MapEntity(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }



    }
}
