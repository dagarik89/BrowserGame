using DataLayer.Models;
using System.Linq;

namespace DataLayer.Data
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
                new Persons { Name = "Test",   Color = "Чёрный+Красный", Speed = 10, Size = 10, User = "galantsev.dmitriy@gmail.com" },
            };
            foreach (Persons s in usersLogin)
            {
                context.Persons.Add(s);
            }
            context.SaveChanges();

        }
    }
}
