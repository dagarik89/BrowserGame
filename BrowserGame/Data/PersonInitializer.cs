using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrowserGame.Models;

namespace BrowserGame.Data
{
    public class PersonInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Persons.Any())
            {
                return;   // DB has been seeded
            }

            var usersLogin = new Persons[]
            {
                new Persons { Name = "Adam",   Age = 30, Strength = 20, Defense = 30, Health = 100 },
                new Persons { Name = "Eva",   Age = 25, Strength = 15, Defense = 35, Health = 100 },
            };
            foreach (Persons s in usersLogin)
            {
                context.Persons.Add(s);
            }
            context.SaveChanges();

        }
    }
}
