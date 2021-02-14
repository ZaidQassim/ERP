using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Appapi.Models;

namespace Appapi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You moust specify password between  4 and 8 characters")]
        public string password { get; set; }

        [Required]
        public string KnownAs { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        //  to  insalase to  value to  prop
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
        }
        public List<OrgUser> OrgID { get; set; }

        
    }

    public class OrgUser
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}