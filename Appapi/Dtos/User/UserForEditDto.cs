using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Appapi.Dtos
{
    public class UserForEditDto
    {
        [Required]
        public string UserName { get; set; }
        public string password { get; set; }

        [Required]
        public string KnownAs { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        //  to  insalase to  value to  prop
        public List<OrgUser> OrgID { get; set; }
    }


}