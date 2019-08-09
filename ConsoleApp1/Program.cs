using ConsoleApp1.ORM;
using Lettuce.DependencyInjection;
using Lettuce.ORM;
using Lettuce.ORM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dapper;
using System.Linq;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace ConsoleApp1
{
    internal class Program
    {
        const int NUM = 200;
        static string connectString = "server=106.13.201.113;user id=root;pwd=Chenlike123_;port=3306;database=LettuceTest;charset=utf8mb4";

        private static void Main(string[] args)
        {

            //Summary summary = BenchmarkRunner.Run<SpeedTest>();
            Console.ReadLine();
        }
    }
}