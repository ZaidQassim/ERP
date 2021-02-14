using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.Dtos
{
    public class ChatDto
    {
        public int RoomID { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public ChatDto()
        {
            Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}
