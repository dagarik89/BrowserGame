using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services.Implementation
{
    internal class PersonsDataService : IPersonsDataService
    {
        private ApplicationDbContext db;
        public PersonsDataService(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task DeleteAsync(int id)
        {
            var persons = await db.Persons.FindAsync(id);
            db.Persons.Remove(persons);
            await db.SaveChangesAsync();
        }

        public async Task<IList<PersonsData>> GetPersons(string name)
        {
            var userPersons = db.Persons
                .Where(m => m.User == name || m.User == "Default");

            return await userPersons.ToListAsync();
        }


        public async Task<PersonsData> GetDetails(int id)
        {
            var persons = db.Persons.AsNoTracking()
                .FirstOrDefaultAsync(m => m.PersonsID == id);

            return await persons;
        }

        public async Task CreatePers(PersonsData persons, string name)
        {
            this.db.Persons.Add(persons);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdatePers(PersonsData persons, string name)
        {
            this.db.Persons.Update(persons);
            await this.db.SaveChangesAsync();
        }

        public  IList<PersonsData> EqualPers(string name)
        {
            IQueryable<PersonsData> equalPers = db.Persons
                        .Where(m => m.Name == name);
            return equalPers.ToList();
        }

        public IList<PersonsData> EqualPersUpdate(string name, int id)
        {
            IQueryable<PersonsData> equalPers = db.Persons
                        .Where(m => m.Name == name && m.PersonsID != id);
            return equalPers.ToList();
        }
    }
}
