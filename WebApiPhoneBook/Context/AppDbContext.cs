using Microsoft.EntityFrameworkCore;
using WebApiPhoneBook.Models;

namespace WebApiPhoneBook.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasDiscriminator<ContactType>("ContactType")
                .HasValue<Person>(ContactType.Person)
                .HasValue<PublicOrganization>(ContactType.PublicOrganization)
                .HasValue<PrivateOrganization>(ContactType.PrivateOrganization);

        }

    }
}
