using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Data.Interfaces;
using Appapi.Dtos;
using Appapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appapi.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DashboardsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public DashboardsController(DataContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


    }
}