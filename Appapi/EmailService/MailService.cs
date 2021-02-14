using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Appapi.Data;
using Microsoft.AspNetCore.Identity;
using Appapi.Models;
using System.Security.Claims;

namespace Appapi.EmailService
{
    public class MailService : IMailService
    {
        private readonly EmailConfiguration _mailSettings;
        private readonly DataContext _Context;
        private readonly UserManager<User> _userManager;
        public MailService(IOptions<EmailConfiguration> mailSettings,DataContext context, UserManager<User> userManager)
        {
            _mailSettings = mailSettings.Value;
            _Context = context;
            _userManager = userManager;

        }
        //public async Task SendEmailAsync(MailRequest mailRequest)
        //{
          
        //    var mail = _Context.EmailConfigurations.Where(q=>q.UserID==2).Select(a => a.Mail).FirstOrDefault();
        //    var password = _Context.EmailConfigurations.Where(q => q.UserID == 2).Select(a => a.Password).FirstOrDefault();
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(mail);
        //    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //    email.Subject = mailRequest.Subject;
        //    var builder = new BodyBuilder();
        //    if (mailRequest.Attachments != null)
        //    {
        //        byte[] fileBytes;
        //        foreach (var file in mailRequest.Attachments)
        //        {
        //            if (file.Length > 0)
        //            {
        //                using (var ms = new MemoryStream())
        //                {
        //                    file.CopyTo(ms);
        //                    fileBytes = ms.ToArray();
        //                }
        //                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //            }
        //        }
        //    }
        //    builder.HtmlBody = mailRequest.Body;
        //    email.Body = builder.ToMessageBody();
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(mail, password);
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //}
    }
}
