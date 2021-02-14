using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Dtos;
using Appapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Appapi.Data.Interfaces;

namespace Appapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;

        public AuthController(IConfiguration config, IMapper mapper,DataContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _context = context;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {

            // to get info for new user
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            // to  save info user in DB
            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.password);

            // return UserForDetailedDto because i not return password 
            var usreToReturn = _mapper.Map<UserForListDto>(userToCreate);


            if (result.Succeeded)
            {
                //  save the user as root Admin
                await _userManager.AddToRolesAsync(userToCreate, new[] { "Root" });
               
                // to  go controller users to Action GetUser 
                // return CreatedAtRoute("GetUsers", new { Controller = "Users" }, usreToReturn);
                return Ok("added user successful");
            }

            return BadRequest();


        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            // userneme and password match  wiht stored in hte database 
            // to  get the user
            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            if (user == null)
                return BadRequest("the user name is not found ");

            // sign in for user 
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);
            
            if (result.Succeeded)
            {
                var appUser = _mapper.Map<UserForListDto>(user);
                var userRoles = await _userManager.GetRolesAsync(user); // get roles for user
                return Ok(new
                {
                    token = GenerateJwtToken(user).Result,
                    user = appUser,
                    roles = userRoles                    
                });
            }
           
            return Unauthorized("Something went wrong");
        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName)
            };

            // to  get role for user that logged In now
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                // add role to token user
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8
                 .GetBytes(_config.GetSection("AppSettings:ToKen").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}