using BrowserGame.Models;
using BrowserGame.ViewModels;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Services.Implementation
{
    internal class PersonsService : IPersonsService
    {
        private readonly IPersonsBusinessService personsData;

        public PersonsService(IPersonsBusinessService personsData)
        {
            this.personsData = personsData;
        }  

        public Task DeletePersonsAsync(int id)
        {
            return this.personsData.DeletePersonsAsync(id);
        }

        public async Task<IEnumerable<Persons>> GetPersons(string name)
        {
            return (await this.personsData.GetPersons(name)).Adapt<IEnumerable<Persons>>();
        }

        public async Task<Persons> GetDetails(int id)
        {
            return (await personsData.GetDetails(id)).Adapt<Persons>();
        }

        public async Task<int> CreatePers(Persons persons, string name, string operation)
        {
            var basePersons = persons.Adapt<PersonsBusiness>();
            basePersons.User = name;
            await this.personsData.CreatePers(basePersons, name, operation);
            return basePersons.PersonsID;
        }

        public IEnumerable<Persons> EqualPers(string name, string operation, int? id)
        {
            return (this.personsData.EqualPers(name, operation, id)).Adapt<IEnumerable<Persons>>();
        }

        public GameViewModel GetGame(Persons persons)
        {
            var basePersons = persons.Adapt<PersonsBusiness>();
            return personsData.GetGame(basePersons).Adapt<GameViewModel>();
        }
    }
}