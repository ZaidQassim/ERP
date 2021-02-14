using Appapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.EmailService
{
    public class MailRequest
    {
        public int ID { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [NotMapped]
        public List<IFormFile> Attachments { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
    public class MailRequestConfiguration : IEntityTypeConfiguration<MailRequest>
    {
        public void Configure(EntityTypeBuilder<MailRequest> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.ToEmail).IsRequired();
            builder.Property(c => c.Subject).IsRequired();
            builder.Property(c => c.Body).IsRequired();
            builder.Property(c => c.UserID).IsRequired();
            //builder.Property(c => c.Attachments).IsRequired();
        }
    }
}
