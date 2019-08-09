using System;
using System.Collections.Generic;
using System.Text;

namespace ILGenerateDebug.Model
{
    public class TestModel1
    {
        public static string B(string a )
        {
            return a;
        }
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";



        public int A(string p)
        {
            return p.Length + Password.Length;
        }
    }
}
