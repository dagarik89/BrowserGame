using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using BusinessLayer.Services.Implementation;
using DataLayer.Models;
using DataLayer.Services;
using Moq;
using NUnit.Framework;

namespace BusinessLayer.Tests
{
    [TestFixture]
    public class PersonsBusinessServiceTests
    {
        Mock<IPersonsDataService> personsDataService;
        PersonsBusinessService personsService;
        PersonsData model;
        PersonsBusiness modelBusiness;

        [SetUp]
        public void Setup()
        {
            // Arrange
            model = new PersonsData { PersonsID = 1, Name = "AAA", Speed = 20, Size = 20 };
            modelBusiness = new PersonsBusiness { PersonsID = 2, Name = "BBB", Color = "Чёрный+зелёный" };

            personsDataService = new Mock<IPersonsDataService>();

            personsService = new PersonsBusinessService(personsDataService.Object);
        }

        private List<PersonsData> GetTestPersons()
        {
            var persons = new List<PersonsData>
            {
                new PersonsData { PersonsID=1, Name="AAA", Speed=20, Size=20},
                new PersonsData { PersonsID=2, Name="BBB", Speed=30, Size=30},
                new PersonsData { PersonsID=3, Name="CCC", Speed=40, Size=40},
            };
            return persons;
        }

        [Test]
        public void DeletePersonsAsync_ReturnTaskOfBool()
        {
            // Arrange

            // Act
            var result = personsService.DeletePersonsAsync(100);

            // Assert
            personsDataService.Verify(m => m.DeleteAsync(100));
            Assert.That(result, Is.TypeOf<Task<bool>>());
        }

        [Test]
        public async Task GetPersons_ReturnListOfPersonsBusinessModel()
        {
            // Arrange
            personsDataService.Setup(m => m.GetPersons("User")).ReturnsAsync(GetTestPersons());

            PersonsBusinessService _personsService = new PersonsBusinessService(personsDataService.Object);

            // Act
            var result = await _personsService.GetPersons("User");

            // Assert
            Assert.AreEqual(GetTestPersons().Count, result.Count);
            Assert.That(result, Is.TypeOf<List<PersonsBusiness>>());
        }

        [Test]
        public async Task GetDetails_ReturnPersonsBusinessModel()
        {
            // Arrange
            personsDataService.Setup(m => m.GetDetails(100)).ReturnsAsync(model);

            PersonsBusinessService _personsService = new PersonsBusinessService(personsDataService.Object);

            // Act
            var result = await _personsService.GetDetails(100);

            // Assert
            Assert.That(result, Is.TypeOf<PersonsBusiness>());
        }

        [Test]
        public void EqualPers_ReturnListOfPersonsBusinessModel()
        {
            // Arrange
            personsDataService.Setup(m => m.EqualPersUpdate("User", 100)).Returns(GetTestPersons());

            PersonsBusinessService _personsService = new PersonsBusinessService(personsDataService.Object);

            // Act
            var result = _personsService.EqualPers("User", "update", 100);

            // Assert
            Assert.AreEqual(GetTestPersons()[0].Size, result[0].Size);
            Assert.That(result, Is.TypeOf<List<PersonsBusiness>>());
        }

        [Test]
        public void GetGame_ReturnGameModel()
        {
            // Arrange

            // Act
            var result = personsService.GetGame(modelBusiness);

            // Assert
            Assert.AreEqual("green", result.Food_color);
            Assert.That(result, Is.TypeOf<GameModel>());
        }

        [Test]
        public void SaveMaxResult_ResultMoreThanMaxPoints_UpdatePers()
        {
            // Arrange
            personsDataService.Setup(m => m.GetDetails(5)).ReturnsAsync(model);

            // Act
            var result = personsService.SaveMaxResult("User", 5, 20);

            // Assert
            personsDataService.Verify(m => m.UpdatePers(model, "User"));
            Assert.AreEqual(model.MaxPoints, 20);
        }
    }
}
