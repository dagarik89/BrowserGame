using System;
using System.Collections.Generic;
using System.Text;
using BrowserGame.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Persons> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Persons>().HasKey(m => m.PersonsID);
            base.OnModelCreating(builder);
        }
    }
}
