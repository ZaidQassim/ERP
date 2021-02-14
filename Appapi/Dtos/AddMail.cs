using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.Dtos
{
    public class AddMail
    {
        public int UserID { get; set; }
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
