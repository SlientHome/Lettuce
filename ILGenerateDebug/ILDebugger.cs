using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILGenerateDebug
{
    public static class ILDebugger
    {
        public static ILCodeClass CreateILCodeClass()
        {
            var assemblyName = new AssemblyName("ILGenerateDebugAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            ILCodeClass iLCodeClass = new ILCodeClass(assemblyBuilder);
            return iLCodeClass;
        }
        public static ILCodeFunction CreateILCodeFunction()
        {
            var assemblyName = new AssemblyName("ILGenerateDebugAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            ILCodeFunction iLCodeClass = new ILCodeFunction(assemblyBuilder);
            return iLCodeClass;
        }
    }
}
