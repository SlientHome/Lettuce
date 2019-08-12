using BenchmarkDotNet.Attributes;
using ConsoleApp1.ORM;
using Dapper;
using Lettuce.ORM;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class SpeedTest
    {
        const int NUM = 100;
        static string connectString = "server=.;database=LettuceTest;integrated security=SSPI";
        [Benchmark]
        public void Dapper()
        {
            using (SqlConnection conn = new SqlConnection(connectString))
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
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                int loop = 0;
                while (loop < NUM)
                {
                    var a = conn.FindList<UserInfo>("select   Id,UserName,Password,CreateTime from UserInfo");
                    loop++;
                }
            }
        }

        [Benchmark]
        public void ADO()
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                int loop = 0;
                while (loop < NUM)
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "select   Id,UserName,Password,CreateTime from UserInfo";
                    var reader = cmd.ExecuteReader();
                    List<UserInfo> list = new List<UserInfo>();

                    while (reader.Read())
                    {
                        UserInfo a = new UserInfo();
                        a.Id = (int)reader.GetValue(0);
                        a.UserName = reader.GetValue(1).ToString();
                        a.Password = reader.GetValue(2).ToString();
                        a.CreateTime = (DateTime)reader.GetValue(3);
                        list.Add(a);
                    }
                    reader.Close();


                    loop++;
                }
            }
        }


    }
}
