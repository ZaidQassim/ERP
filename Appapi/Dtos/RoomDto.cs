using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.Dtos
{
    public class RoomDto
    {
        public int SenderID { get; set; }
        public int RecipintID { get; set; }
        public DateTime Date { get; set; }
        public RoomDto()
        {
            Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}
