using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILGenerateDebug
{
    public class ILCodeFunction : BaseCode
    {
        private TypeBuilder typeBuilder { get; set; }
        public ILGenerator IL { get; set; }
        public ILCodeFunction(AssemblyBuilder builder) : base(builder)
        {
            var moduleBuilder = Builder.DefineDynamicModule("DebugModule");
            typeBuilder = moduleBuilder.DefineType("ILDebugClass", TypeAttributes.Public);
            // create method builder
            var methodBuilder = typeBuilder.DefineMethod(
              "I_AM_FUNCTION",
              MethodAttributes.Public | MethodAttributes.Static,
              null,
              null);

            IL = methodBuilder.GetILGenerator();

        }

        public new string GenerateCSharp()
        {
             typeBuilder.CreateType();
            return base.GenerateCSharp();
        }

    }
}
