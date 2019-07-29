using ConsoleApp1.ORM;
using Lettuce.DependencyInjection;
using Lettuce.ORM;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connectString  = "server=132.232.25.90;user id=root;pwd=Chenlike123_;port=3306;database=LettuceTest;charset=utf8mb4";

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = " select * from UserInfo ";

                
                IDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                var t = typeof(IDataReader);
                var meds = t.GetMethods();




                //EntityMapping.MapReaderToModel<UserInfo>(dataReader);

                Console.WriteLine();


            }



            Console.ReadLine();
        }
    }
}