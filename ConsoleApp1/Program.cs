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

namespace ConsoleApp1
{
    internal class Program
    {
        const int NUM = 200;
        static string connectString = "server=106.13.201.113;user id=root;pwd=Chenlike123_;port=3306;database=LettuceTest;charset=utf8mb4";
        static void LettuceORM()
        {
            Console.WriteLine("LettuceORM start");
            long sum = 0;

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                int loop = 0;
                while (loop < NUM)
                {
                    Stopwatch s1 = new Stopwatch();
                    s1.Start();

                    var a = conn.QuerySql<UserInfo>("select Id,UserName,Password,CreateTime from UserInfo");
                    //var a = conn.Query<UserInfo>("select Id,UserName,Password,CreateTime from UserInfo").ToList();
                    s1.Stop();
                    //Console.WriteLine(s1.ElapsedMilliseconds);
                    loop++;
                    if (loop == 1)
                    {
                        Console.WriteLine($"LettuceORM first query {s1.ElapsedMilliseconds}");
                    }
                    sum += s1.ElapsedMilliseconds;
                }

            }
            Console.WriteLine($"LettuceORM sum:{sum} avg:{sum/NUM}");
            Console.WriteLine("LettuceORM end");
        }

        static void Dapper()
        {
            Console.WriteLine("Dapper start");
            long sum = 0;
            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                int loop = 0;
                while (loop < NUM)
                {
                    Stopwatch s1 = new Stopwatch();
                    s1.Start();

                    //var a = conn.QuerySql<UserInfo>("select Id,UserName,Password,CreateTime from UserInfo");
                    var a = conn.Query<UserInfo>("select Id,UserName,Password,CreateTime from UserInfo").ToList();
                    s1.Stop();
                    //Console.WriteLine(s1.ElapsedMilliseconds);
                    loop++;
                    if (loop == 1)
                    {
                        Console.WriteLine($"Dapper first query {s1.ElapsedMilliseconds}");
                    }
                    sum += s1.ElapsedMilliseconds;
                }

            }
            Console.WriteLine($"Dapper sum:{sum} avg:{sum / NUM}");
            Console.WriteLine("Dapper end");
        }

        private static void Main(string[] args)
        {
            //LettuceORM();
            //Dapper();

            //Dapper();
            LettuceORM();
            Console.ReadLine();
        }
    }
}