using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();

            builder.Property(c => c.RoomID);
            builder.Property(c => c.Body).IsRequired();
            builder.Property(c => c.Attachment);
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.IsRead).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
