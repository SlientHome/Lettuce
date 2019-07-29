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
        public List<string> FieldsList = new List<string>();
        private Func<IDataReader, TEntity> MapFunc { get; set; } = null;
        public Func<IDataReader, TEntity> GenerateEntityMapperFunc()
        {
            var type = typeof(TEntity);
            var propertyInfos = type.GetProperties().Where(t=> t.GetSetMethod()!=null).ToList();

            DynamicMethod dymMethod = new DynamicMethod("GetEntity_PropertyMethod_"+type.Name, type, new Type[] { typeof(IDataReader) }, true);
            // 对象 默认无参构造函数
            ConstructorInfo entityConstructorInfo = typeof(TEntity).GetConstructors().FirstOrDefault(t => t.GetParameters().Length == 0); ;

            ILGenerator il = dymMethod.GetILGenerator();
            il.DeclareLocal(typeof(TEntity));// 局部变量位置 0
            // 存到局部变量 第0个位置
            il.Emit(OpCodes.Newobj, entityConstructorInfo);
            il.Emit(OpCodes.Stloc, 0);
            il.Emit(OpCodes.Ldloc, 0);
            il.Emit(OpCodes.Box, typeof(TEntity));
            il.Emit(OpCodes.Ret);



           // typeof(IDataReader).GetMethods().FirstOrDefault(t=>t.)

            return null;
        }

        public TEntity MapEntity(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }



    }
}
