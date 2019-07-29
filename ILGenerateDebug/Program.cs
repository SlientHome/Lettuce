using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.OutputVisitor;
using System;
using System.IO;
using System.Reflection.Emit;

namespace ILGenerateDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            var target = ILDebugger.CreateILCodeFunction();
            var il = target.IL;

            /* while起点  */
            Label whileStartLabel = il.DefineLabel();
            Label whileIfLable = il.DefineLabel();


            il.DeclareLocal(typeof(int));
            il.Emit(OpCodes.Ldc_I4, 5);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br, whileIfLable);
            il.MarkLabel(whileStartLabel);
            il.Emit(OpCodes.Ldstr, "我废了");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Ldloc, 0);
            il.Emit(OpCodes.Ldc_I4, 1);
            il.Emit(OpCodes.Sub);
            il.Emit(OpCodes.Stloc, 0);
            il.MarkLabel(whileIfLable);
            il.Emit(OpCodes.Ldloc, 0);
            il.Emit(OpCodes.Ldc_I4, 0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Brfalse, whileStartLabel);
            il.Emit(OpCodes.Ret);

            string code = target.GenerateCSharp();
            Console.WriteLine();
        }
    }
}
