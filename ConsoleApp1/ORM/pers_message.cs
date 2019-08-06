using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.ORM
{
    public class pers_message
    {
        public int MessageId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public string MessageType { get; set; }
        public string RecipientId { get; set; }
        public DateTime SendDate { get; set; }

    }
}
