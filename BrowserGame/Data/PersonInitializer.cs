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
                new Persons { Name = "Ультра",   Color = "Чёрный+Красный", Speed = 50, Size = 30, User = "Default" },
                new Persons { Name = "Черепаха",   Color = "Чёрный+Красный", Speed = 10, Size = 30, User = "Default" },
            };
            foreach (Persons s in usersLogin)
            {
                context.Persons.Add(s);
            }
            context.SaveChanges();

        }
    }
}
