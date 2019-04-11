using Lettuce.DependencyInjection.Attributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lettuce.DependencyInjection
{
    public class LettuceContainer : ILettuceContainer
    {
        public void Register<TAbstracter, TImplementer>()
        {
            LettuceContainerStore<TAbstracter>.InitClassType<TImplementer>();
        }

        public TAbstracter Resolver<TAbstracter>()
        {
            var abstracterType = typeof(TAbstracter);
            var Implementer = Create(abstracterType);
            return (TAbstracter)Implementer;
        }

        private object Create(Type abstracterType)
        {
            var ImplementerType = GetImplementerType(abstracterType);
            if (ImplementerType == null)
            {
                return null;
            }
            //构造函数注入
            //获取构造函数
            var ctorInfo = ImplementerType.GetConstructors().OrderByDescending(p => p.GetParameters().Count()).FirstOrDefault();
            var parameterInfos = ctorInfo.GetParameters();
            List<object> args = new List<object>();
            foreach (var parameterInfo in parameterInfos)
            {
                args.Add(Create(parameterInfo.ParameterType));
            }
            var Implementer = Activator.CreateInstance(ImplementerType, args.ToArray());

            //属性注入
            var propertyInfos = ImplementerType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyObject = Create(propertyInfo.PropertyType);
                propertyInfo.SetValue(Implementer, propertyObject);
            }

            //方法注入
            var methodInfos = ImplementerType.GetMethods();
            foreach (var methodInfo in methodInfos.Where(t=>t.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ImportAttribute) )!=null))
            {
                List<object> paramsList = new List<object>();
                foreach (var param in methodInfo.GetParameters())
                {
                    paramsList.Add(Create(param.ParameterType));
                }
                methodInfo.Invoke(Implementer, paramsList.ToArray());
            }

            return Implementer;
        }

        private Type GetImplementerType(Type abstracterType)
        {
            Type typeContainerStore = typeof(LettuceContainerStore<>);
            typeContainerStore = typeContainerStore.MakeGenericType(abstracterType);

            var methodInfo = typeContainerStore.GetMethod("GetClassType");
            var containerStore = Activator.CreateInstance(typeContainerStore);
            var implementerType = methodInfo.Invoke(containerStore, null);
            return (Type)implementerType;
        }
    }
}