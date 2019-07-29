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

            il.Emit(OpCodes.Ldstr, "我废了");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ret);

            string code = target.GenerateCSharp();
            Console.WriteLine();
        }
    }
}
