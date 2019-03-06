using DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserData>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<PersonsData> Persons { get; set; }
        public DbSet<UserData> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PersonsData>().HasKey(m => m.PersonsID);
            builder.Entity<UserData>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
