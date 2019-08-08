using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.OutputVisitor;
using ILGenerateDebug.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace ILGenerateDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            var target = ILDebugger.CreateILCodeFunction();
            var il = target.IL;

            il.DeclareLocal(typeof(Type));

            il.Emit(OpCodes.Ldtoken, typeof(int));
            il.Emit(OpCodes.Stloc, 0);

            il.Emit(OpCodes.Ret);
            string code = target.GenerateCSharp();
            Console.WriteLine();
        }
    }
}
