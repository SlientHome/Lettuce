using Lettuce.ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.ORM
{

    public class UserInfo
    {       
        [PrimaryKey]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
