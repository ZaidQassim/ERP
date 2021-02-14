using Appapi.Data.EntityConfiguration;
using Appapi.EmailService;
using Appapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Appapi.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<MailRequest> MailRequests { get; set; }
        public DbSet<EmailConfiguration> EmailConfigurations { get; set; }

        // Employes
        public DbSet<Employe> Employes { get; set; }
        public DbSet<EmployeAccord> EmployeAccords { get; set; }
        public DbSet<EmployeAccordAttachment> EmployeAccordAttachments { get; set; }
        public DbSet<EmployeAttachment> EmployeAttachments { get; set; }
        public DbSet<EmployeEmail> EmployeEmails { get; set; }
        public DbSet<EmployePaymentCard> EmployePaymentCards { get; set; }
        public DbSet<EmployeTelephoneNumber> EmployeTelephoneNumbers { get; set; }

        // Customer
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccord> CustomerAccords { get; set; }
        public DbSet<CustomerAccordAttachment> CustomerAccordAttachments { get; set; }
        public DbSet<CustomerAttachment> CustomerAttachments { get; set; }
        public DbSet<CustomerEmail> CustomerEmails { get; set; }
        public DbSet<CustomerTelephoneNumber> CustomerTelephoneNumbers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EmployeConfigration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfigration());

        }
    }
}