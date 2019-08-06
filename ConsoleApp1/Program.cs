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
            string connectString  = "server=132.232.25.90;user id=root;pwd=Chenlike123_;port=3306;database=LettuceTest;charset=utf8mb4";

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                for (int loop = 0; loop < 10; loop++)
                {
                    Console.WriteLine($"loop:{loop+1}");
                    Stopwatch s1 = new Stopwatch();
                    s1.Start();
                    for (int i = 0; i < NUM; i++)
                    {
                        var list = conn.Query<UserInfo>("select Id,UserName,Password from UserInfo").AsList();
                    }
                    s1.Stop();
                    Console.WriteLine("Dapper:"+s1.ElapsedMilliseconds);
                    Stopwatch s2 = new Stopwatch();
                    s2.Start();
                    for (int i = 0; i < NUM; i++)
                    {
                        var list = conn.QuerySql<UserInfo>("select Id,UserName,Password from UserInfo");
                    }
                    s2.Stop();
                    Console.WriteLine("My:" + s2.ElapsedMilliseconds);
                }
            }



            Console.WriteLine($"over ");
            Console.ReadLine();
        }
    }
}