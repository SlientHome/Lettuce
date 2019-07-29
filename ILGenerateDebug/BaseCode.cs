using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.OutputVisitor;
using Lokad.ILPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Text;

namespace ILGenerateDebug
{
    public class BaseCode
    {
        public AssemblyBuilder Builder { get; set; }
        public BaseCode(AssemblyBuilder builder)
        {
            Builder = builder;
        }
        public string GenerateCSharp()
        {
            string dllPath = $"./{Guid.NewGuid().ToString()}.dll";
            var generator = new AssemblyGenerator();
            generator.GenerateAssembly(Builder, dllPath);
            CSharpDecompiler decompiler = new CSharpDecompiler(dllPath, new ICSharpCode.Decompiler.DecompilerSettings());
            var syntaxTree = decompiler.DecompileWholeModuleAsSingleFile();
            StringWriter output = new StringWriter();
            var visitor = new CSharpOutputVisitor(output, FormattingOptionsFactory.CreateSharpDevelop());
            syntaxTree.AcceptVisitor(visitor);
            return output.ToString();
        }
    }
}
