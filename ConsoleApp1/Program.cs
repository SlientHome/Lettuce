using ConsoleApp1.ORM;
using Lettuce.DependencyInjection;
using Lettuce.ORM;
using Lettuce.ORM.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

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
                command.CommandText = " select Id,UserName,Password from UserInfo where id='123123'";

                
                IDataReader dataReader = command.ExecuteReader();
                dataReader.Read();


                List<FieldEntityInfo> list = new List<FieldEntityInfo>();

                list.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "Id",
                    FieldInDbType = typeof(int),
                    SetMethod = typeof(UserInfo).GetMethod("set_Id"),
                    Index = 0
                });
                list.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "UserName",
                    FieldInDbType = typeof(string),
                    SetMethod = typeof(UserInfo).GetMethod("set_UserName"),
                    Index = 1
                });
                list.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "Password",
                    FieldInDbType = typeof(string),
                    SetMethod = typeof(UserInfo).GetMethod("set_Password"),
                    Index = 2
                });



                EntityDataMapping<UserInfo> entityData = new EntityDataMapping<UserInfo>(list);



                var function = entityData.GenerateEntityMapperFunc();
                var a = function(dataReader);
                Console.WriteLine();


            }


            Console.WriteLine("over");
            Console.ReadLine();
        }
    }
}