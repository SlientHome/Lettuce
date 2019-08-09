using BenchmarkDotNet.Attributes;
using ConsoleApp1.ORM;
using Dapper;
using Lettuce.ORM;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class SpeedTest
    {
        const int NUM = 200;
        static string connectString = "server=106.13.201.113;user id=root;pwd=Chenlike123_;port=3306;database=LettuceTest;charset=utf8mb4";
        [Benchmark]
        public void Dapper()
        {
            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                int loop = 0;
                while (loop < NUM)
                {
                    var a = conn.Query<UserInfo>("select  Id,UserName,Password,CreateTime from UserInfo").ToList();
                    loop++;
                }
            }
        }
        [Benchmark]
        public void Lettuce()
        {
            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                int loop = 0;
                while (loop < NUM)
                {
                    var a = conn.FindList<UserInfo>("select   Id,UserName,Password,CreateTime from UserInfo");
                    loop++;
                }
            }
        }
    }
}
