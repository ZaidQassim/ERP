using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Data.Interfaces;
using Appapi.Dtos;
using Appapi.EmailService;
using Appapi.Hubs;
using Appapi.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Appapi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork<User> _user;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailService mailService;
        private readonly EmailConfiguration _mailSettings;
        private readonly IHubContext<SignalRHub> _hubContext;

        public UsersController(IOptions<EmailConfiguration> mailSettings, IUnitOfWork<User> user, IMapper mapper, DataContext context, UserManager<User> userManager, IMailService mailService, IHubContext<SignalRHub> hubContext)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
            _userManager = userManager;
            this.mailService = mailService;
            _mailSettings = mailSettings.Value;
            _hubContext = hubContext;
        }


        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers(int? skip, int? take)
        {
            if (!skip.HasValue) skip = 0;
            if (!take.HasValue) take = 0;

            var count = await _user.Entity.Count();
            var users = await _user.Entity.GetAll(skip.Value, take.Value);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return StatusCode(200, new
            {
                code = 200,
                message = "getUsers successfuly",
                result = new { usersToReturn, count }
            });
        }


        [HttpGet("getUsersById/{id}")]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var user = await _user.Entity.GetById(id);
            if (user == null)
                return BadRequest("the itemm is not found");
            var usersToReturn = _mapper.Map<UserForDetailsDto>(user);

            return StatusCode(200, new
            {
                code = 200,
                message = "successfuly",
                result = new
                {
                    usersToReturn,
                }
            });
        }

        // [AllowAnonymous]
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] UserForRegisterDto model)
        {
            var User = _mapper.Map<User>(model);

            // var lt = new List<OrganizationUser>();
            // foreach (var m in model.OrgID)
            // {
            //     lt.Add(new OrganizationUser() { OrginaztionId = m.Id, UserId = User.Id, IsActive = m.IsActive });
            // }
            // User.OrganizationUsers = lt;

            var result = await _userManager.CreateAsync(User, model.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(User, new[] { "Admin" });
                return StatusCode(200, new
                {
                    code = 200_002,
                    message = "successfuly ",
                    result = new
                    {
                        User.Id
                    }
                });
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("editUser/{id}")]
        public async Task<IActionResult> EditUser(string id, [FromBody] UserForEditDto model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("the item is not found");

            var userForEdit = _mapper.Map(model, user);

            if (model.password != null)
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.password);
            }

            await _userManager.UpdateAsync(user);

            return StatusCode(200, new
            {
                code = 200,
                message = "edit successfuly",
                result = new { user.Id }
            });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _user.Entity.GetById(id);
            if (user == null)
                return NotFound("the item is not found");

            user.IsDelete = true;
            await _user.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "delete successfuly",
                result = new { user.Id }
            });
        }


        [HttpPost("room")]
        public async Task<IActionResult> Chat([FromBody] RoomDto model)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (isCurrentUser == null)
                return Unauthorized();
            model.SenderID = int.Parse(isCurrentUser);

            var room = _mapper.Map<Room>(model);


            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("room", room.ID, room.Date);
            return StatusCode(201, new
            {
                code = 201,
                message = " Successfuly",
                result = new
                {
                    ID = room.ID,

                }

            });
        }


        [HttpGet("room")]
        public async Task<IActionResult> getrooms(int id)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (isCurrentUser == null)
                return Unauthorized();
            var room = await _context.Rooms.AsNoTracking().Where(z => z.SenderID == int.Parse(isCurrentUser)).Select(f => new
            {
                f.ID,
                reciver = _context.Users.AsNoTracking().Where(a => a.Id == f.RecipintID).Select(w => w.UserName).ToList(),
                f.Date

            }).OrderByDescending(w => w.Date).ToListAsync();

            await _hubContext.Clients.All.SendAsync("getroom", true);
            return StatusCode(200, new
            {
                code = 200,
                message = " Successfuly",
                result = new
                {
                    room,
                }
            });
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatDto model)
        {
            var chat = _mapper.Map<Chat>(model);
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("chat", chat.ID, chat.RoomID, chat.Body, chat.Attachment, chat.Date);
            return StatusCode(201, new
            {
                code = 201,
                message = " Successfuly",
                result = new
                {
                    ID = chat.ID,
                }

            });
        }


        [HttpGet("chat")]
        public async Task<IActionResult> getchat(int id)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (isCurrentUser == null)
                return Unauthorized();
            var chat = await _context.Chats.AsNoTracking().Where(q => q.Room.SenderID == int.Parse(isCurrentUser)).Select(f => new
            {
                f.RoomID,
                f.Body,
                f.Attachment,
                f.IsDeleted,
                f.Date,
                f.Room.SenderID,
                f.Room.Sender.FullName,
                f.Room.RecipintID,
                rec = f.Room.Recipint.FullName
            }).OrderByDescending(w => w.Date).ToListAsync();

            await _hubContext.Clients.All.SendAsync("getchat", true);
            return StatusCode(200, new
            {
                code = 200,
                message = " Successfuly",
                result = new
                {
                    chat
                }
            });
        }


        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest mailRequest)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (isCurrentUser == null)
                return Unauthorized();


            var mail = _context.EmailConfigurations.Where(q => q.UserID == int.Parse(isCurrentUser)).Select(a => a.Mail).FirstOrDefault();
            var password = _context.EmailConfigurations.Where(q => q.UserID == int.Parse(isCurrentUser)).Select(a => a.Password).FirstOrDefault();
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            mailRequest.UserID = int.Parse(isCurrentUser);
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mail, password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            await _context.MailRequests.AddAsync(mailRequest);

            return StatusCode(200, new
            {
                code = 200,
                message = " Successfuly sent",
                result = new
                {
                    mailRequest.ID
                }
            });


        }


        [HttpGet("GetResponse")]
        public async Task<IActionResult> GetResponse(int? skip, int? take)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (isCurrentUser == null)
                return Unauthorized();
            if (!skip.HasValue) skip = 0;
            if (!take.HasValue) take = 0;
            var count = await _context.MailRequests.AsNoTracking().Where(w => w.UserID == int.Parse(isCurrentUser)).CountAsync();
            var info = await _context.MailRequests.Where(w => w.UserID == int.Parse(isCurrentUser)).Select(q => new
            {
                q.ID,
                q.ToEmail,
                q.Subject,
                q.Body,
            }).Skip(take.Value * skip.Value).Take(take.Value).ToListAsync();

            if (info == null)
                return NotFound();
            return StatusCode(200, new
            {
                code = 200,
                message = " Successfuly",
                result = new
                {
                    info,
                    count
                }
            });




        }

     

        [HttpPost("setupMail")]
        public async Task<IActionResult> setupMail([FromBody] AddMail model)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (isCurrentUser == null)
                return Unauthorized();
            model.UserID = int.Parse(isCurrentUser);
            var mail = _mapper.Map<EmailConfiguration>(model);
            await _context.EmailConfigurations.AddAsync(mail);
            await _context.SaveChangesAsync();
            return StatusCode(201, new
            {
                code = 201,
                message = " Successfuly",
                result = new
                {
                    mail.ID

                }
            });
        }

    }
}