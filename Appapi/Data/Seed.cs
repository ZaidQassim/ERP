using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Appapi.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Appapi.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                // create some roles
                var roles = new List<Role>
                {
                    new Role{ Name = "Root"},
                    new Role{ Name = "Admin"},
                    new Role{ Name = "Director"},
                };

                // add this role to  table role
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                // Create root user system
                var rootUser = new User
                {
                    UserName = "root",
                    FullName = "Root Stystem",
                    Email = "zaidqassim12@gmail.com",
                    Created = DateTime.Now,
                    PhoneNumber = "07812345678",
                };
                // to save the info userRoot and password
                var result = userManager.CreateAsync(rootUser, "root").Result;
                userManager.AddToRoleAsync(rootUser, "Root").Wait();
            }


        }


    }
}