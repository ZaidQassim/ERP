using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Identity;

namespace Appapi.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

    }
}