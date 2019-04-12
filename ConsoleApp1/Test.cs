using Lettuce.DependencyInjection.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Test : ITest
    {
        public ITest2 test22 { get; set; }

        private ITest2 test2;

        public Test(ITest2 test2)
        {
            this.test2 = test2;
        }

        public void Say()
        {
            Console.WriteLine(this.GetType().Name);
            this.test2.Say();
            test22.Say();
        }


        [Import]
        public void MethodInInjection(ITest2 t2,int a)
        {
            t2.Say();
        }


    }
}