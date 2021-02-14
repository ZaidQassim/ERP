using System;
using System.Collections.Generic;

namespace Appapi.Dtos
{
    public class UserForDetailsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string KnownAs { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public int CountOrgForUser { get; set; }
        public int CountDoucmentForUser { get; set; }
        public int CountSenderDoucmentForUser { get; set; }
        public int CountResipentDoucmentForUser { get; set; }
        public int CountCommentDoucmentForUser { get; set; }

    }
}