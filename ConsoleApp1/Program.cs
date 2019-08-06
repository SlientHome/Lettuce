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
                command.CommandText = " select Id,UserName,Password from UserInfo ";

                
                IDataReader dataReader = command.ExecuteReader();
                dataReader.Read();

                EntityDataMapping<UserInfo> entityData = new EntityDataMapping<UserInfo>();
                entityData.ExistFieldsList.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "Id",
                    FieldInDbType = typeof(int),
                    SetMethod = typeof(UserInfo).GetMethod("set_Id")
                });
                entityData.ExistFieldsList.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "UserName",
                    FieldInDbType = typeof(string),
                    SetMethod = typeof(UserInfo).GetMethod("set_UserName")
                });
                entityData.ExistFieldsList.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "Password",
                    FieldInDbType = typeof(string),
                    SetMethod = typeof(UserInfo).GetMethod("set_Password")
                });
                entityData.ExistFieldsList.Add(new Lettuce.ORM.Model.FieldEntityInfo()
                {
                    FieldName = "CreateTime",
                    FieldInDbType = typeof(DateTime),
                    SetMethod = typeof(UserInfo).GetMethod("set_CreateTime")
                });



                var function = entityData.GenerateEntityMapperFunc();
                var a = function(dataReader);
                Console.WriteLine();



            }



            Console.ReadLine();
        }
    }
}