using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appapi.Models
{
    public class Room
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public virtual User Sender { get; set; }
        public int RecipintID { get; set; }
        public virtual User Recipint { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();

            builder.Property(c => c.SenderID).IsRequired();
            builder.Property(c => c.RecipintID).IsRequired();
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
