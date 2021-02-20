using System;
using System.Collections.Generic;

namespace Appapi.Dtos
{
    public class UserForDetailsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

    }
}