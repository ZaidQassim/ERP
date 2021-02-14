using System;
using Appapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appapi.Data.EntityConfiguration
{
    public class EmployeConfigration: IEntityTypeConfiguration<Employe>
    {
        public void Configure(EntityTypeBuilder<Employe> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.SecondName).IsRequired();
            builder.Property(c => c.IsDelete).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.Created).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}