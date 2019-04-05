using BusinessLayer.Models;
using BusinessLayer.Services;
using DataLayer.Models;
using DataLayer.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementation
{
    public class PersonsBusinessService : IPersonsBusinessService
    {
        private readonly IPersonsDataService personsServices;

        public PersonsBusinessService(IPersonsDataService personsServices)
        {
            this.personsServices = personsServices;
        }

        public Task DeletePersonsAsync(int id)
        {
            return this.personsServices.DeleteAsync(id);
        }

        public async Task<IList<PersonsBusiness>> GetPersons(string name)
        {
            var personsDto = await this.personsServices.GetPersons(name);
            var persons = new List<PersonsBusiness>();
            foreach (var el in personsDto)
            {
                var person = el.Adapt<PersonsBusiness>();
                persons.Add(person);
            }
            return persons;
        }

        public async Task<PersonsBusiness> GetDetails(int id)
        {
            var personsDto = await this.personsServices.GetDetails(id);
            return personsDto.Adapt<PersonsBusiness>();
        }

        public async Task CreatePers(PersonsBusiness persons, string name, string operation)
        {
            var personsDto = persons.Adapt<PersonsData>();
            personsDto.User = persons.User;
            switch (operation)
            {
                case "add":
                    await this.personsServices.CreatePers(personsDto, name);
                    break;
                case "update":
                    await this.personsServices.UpdatePers(personsDto, name);
                    break;
            }
        }

        public IList<PersonsBusiness> EqualPers(string name, string operation, int? id)
        {
            IList<PersonsData> personsDto = null;

            switch (operation)
            {
                case "add":
                    personsDto = this.personsServices.EqualPers(name);
                    break;
                case "update":
                    personsDto = this.personsServices.EqualPersUpdate(name, (int)id);
                    break;
            }

            var persons = new List<PersonsBusiness>();
            foreach (var el in personsDto)
            {
                var person = el.Adapt<PersonsBusiness>();
                persons.Add(person);
            }
            return persons;
        }

        public GameModel GetGame(PersonsBusiness persons)
        {
            string snake_color, food_color;

            switch (persons.Color)
            {
                case "Чёрный+красный":
                    snake_color = "#000";
                    food_color = "red";
                    break;
                case "Зелёный+красный":
                    snake_color = "green";
                    food_color = "red";
                    break;
                case "Синий+красный":
                    snake_color = "blue";
                    food_color = "red";
                    break;
                case "Чёрный+зелёный":
                    snake_color = "#000";
                    food_color = "green";
                    break;
                case "Синий+зелёный":
                    snake_color = "blue";
                    food_color = "green";
                    break;
                case "Красный+зелёный":
                    snake_color = "red";
                    food_color = "green";
                    break;
                default:
                    snake_color = "#000";
                    food_color = "red";
                    break;
            }

            GameModel model = new GameModel
            {
                Speed = persons.Speed,
                Size = persons.Size,
                Snake_color = snake_color,
                Food_color = food_color,
                MaxPoints = persons.MaxPoints,
                PersonID = persons.PersonsID
            };

            return model;
        }
    }
}
