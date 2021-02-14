using Appapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.EmailService
{
    public class EmailConfiguration
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class EmailConfigurationConfiguration : IEntityTypeConfiguration<EmailConfiguration>
    {
        public void Configure(EntityTypeBuilder<EmailConfiguration> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.UserID).IsRequired();
            builder.Property(c => c.Mail).IsRequired();
            builder.Property(c => c.DisplayName).IsRequired();
            builder.Property(c => c.Password).IsRequired();
            //builder.Property(c => c.Attachments).IsRequired();

            
        }
    }
}
