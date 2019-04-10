using Lettuce.DependencyInjection;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ILettuceContainer lettuceContainer = new LettuceContainer();

            lettuceContainer.Register<ITest, Test>();

            lettuceContainer.Register<ITest2, Test2>();
            var test = lettuceContainer.Resolver<ITest>();

            test.Say();

            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }
}