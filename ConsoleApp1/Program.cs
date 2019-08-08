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
namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int NUM = 100;
            string connectString  = "server=106.13.201.113;user id=root;pwd=Chenlike123_;port=3306;database=LettuceTest;charset=utf8mb4";

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                conn.Open();
                Stopwatch s1 = new Stopwatch();
                s1.Start();
                var a  = conn.QuerySql<UserInfo>("select Id,UserName,Password,CreateTime from UserInfo");
                s1.Stop();
                Console.WriteLine(s1.ElapsedMilliseconds);

            }



            Console.WriteLine($"over ");
            Console.ReadLine();
        }
    }
}