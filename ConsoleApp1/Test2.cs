using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Test2 : ITest2
    {
        public void Say()
        {
            Console.WriteLine(this.GetType().Name);
        }
    }
}