using BrowserGame.Controllers;
using BrowserGame.Models;
using BrowserGame.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BrowserGame.ViewModels;
using System.Linq;

namespace BrowserGame.Tests
{
    [TestFixture]
    public class PersonsControllerTests
    {
        PersonsController controller;
        PersonsController controllerWithContext;
        Mock<ILogger<PersonsController>> log;
        Mock<IPersonsService> persons;
        Persons model;

        [SetUp]
        public void Setup()
        {
            // Arrange
            model = new Persons { PersonsID = 1, Name = "Pers", User = "User", Size = 20, Speed = 20 };

            log = new Mock<ILogger<PersonsController>>();
            persons = new Mock<IPersonsService>();

            controller = new PersonsController(log.Object, persons.Object);

            controllerWithContext = new PersonsController(log.Object, persons.Object);
            controllerWithContext.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, "someAuthTypeName"))
                }
            };
        }

        private List<Persons> GetTestPersons()
        {
            var persons = new List<Persons>
            {
                new Persons { PersonsID=1, Name="AAA", Speed=20, Size=20},
                new Persons { PersonsID=2, Name="BBB", Speed=30, Size=30},
                new Persons { PersonsID=3, Name="CCC", Speed=40, Size=40},
            };
            return persons;
        }

        [Test]
        public async Task Index_GetTestPersons_ReturnViewResult()
        {
            // Arrange
            persons.Setup(m => m.GetPersons("User")).ReturnsAsync(GetTestPersons());

            var contr = new PersonsController(log.Object, persons.Object);
            contr.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, "someAuthTypeName"))
                }
            };

            // Act
            var result = await contr.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.IsAssignableFrom<List<Persons>>((result as ViewResult).Model);
            Assert.AreEqual(GetTestPersons().Count, ((result as ViewResult).Model as List<Persons>).Count);
            Assert.AreEqual("CCC", ((result as ViewResult).Model as List<Persons>)[2].Name);
        }

        [Test]
        public async Task Index_ReturnPersonsArray()
        {
            // Arrange

            // Act
            var result = await controllerWithContext.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.IsAssignableFrom<Persons[]>((result as ViewResult).Model);
        }

        [Test]
        public async Task Details_ValidModel_ReturnViewResult()
        {
            // Arrange
            persons.Setup(m => m.GetDetails(100)).ReturnsAsync(new Persons { User = "User", Size=20, Speed=20});
            
            var contr = new PersonsController(log.Object, persons.Object);
            contr.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, "someAuthTypeName"))
                }
            };

            // Act
            var result = await contr.Details(100);

            // Assert
            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.AreEqual("User", ((result as PartialViewResult).Model as Persons).User);
        }

        [Test]
        public async Task Details_IdIsNull_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Details(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_PersonsIsNull_ReturnBadRequestResult()
        {
            // Arrange

            // Act
            var result = await controller.Details(100);

            // Arrange
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Game_IdIsNull_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Game(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Game_PersonsIsNull_ReturnBadRequestResult()
        {
            // Arrange

            // Act
            var result = await controller.Game(100);

            // Arrange
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Game_ValidModel_ReturnGameViewModel()
        {
            // Arrange
            persons.Setup(m => m.GetDetails(100)).ReturnsAsync(model);
            persons.Setup(m => m.GetGame(model)).Returns(new GameViewModel { Size = 40, Speed = 40 });

            var contr = new PersonsController(log.Object, persons.Object);
            contr.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, "someAuthTypeName"))
                }
            };

            // Act
            var result = await contr.Game(100);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual(40, ((result as ViewResult).Model as GameViewModel).Size);
        }

        [Test]
        public async Task Create_InvalidModel_ReturnErrorMessage()
        {
            // Arrange
            Persons modelInvalid = new Persons { Name = "A" };

            //Act
            var result = await controller.Create(modelInvalid);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual(controller.ModelState
                .Values
                .First()
                .Errors[0]
                .ErrorMessage, "Длина строки должна быть от 3 до 10 символов");
        }

        [Test]
        public async Task Create_EqualNames_ReturnErrorMessage()
        {
            // Arrange
            persons.Setup(m => m.EqualPers(model.Name,"add",null)).Returns(GetTestPersons());
            var contr = new PersonsController(log.Object, persons.Object);

            //Act
            var result = await contr.Create(model);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual(contr.ModelState
                .Values
                .First()
                .Errors[0]
                .ErrorMessage, "Персонаж с таким именем занят!");
        }

        [Test]
        public async Task Create_ValidModel_ReturnRedirectToAction()
        {
            // Arrange

            //Act
            var result = await controllerWithContext.Create(model);

            //Assert
            persons.Verify(m => m.CreatePers(model, "User", "add"));
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Edit_IdIsNull_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Edit(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_PersonsIsNull_ReturnBadRequestResult()
        {
            // Arrange

            // Act
            var result = await controller.Edit(100);

            // Arrange
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Edit_EqualNames_ReturnErrorMessage()
        {
            // Arrange
            persons.Setup(m => m.EqualPers(model.Name, "update", model.PersonsID)).Returns(GetTestPersons());

            var contr = new PersonsController(log.Object, persons.Object);
            contr.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, "someAuthTypeName"))
                }
            };

            //Act
            var result = await contr.Edit(model.PersonsID, model);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual(contr.ModelState
                .Values
                .First()
                .Errors[0]
                .ErrorMessage, "Персонаж с таким именем занят!");
        }

        [Test]
        public async Task Edit_ValidModel_ReturnRedirectToAction()
        {
            // Arrange

            //Act
            var result = await controllerWithContext.Edit(model.PersonsID, model);

            //Assert
            persons.Verify(m => m.CreatePers(model, "User", "update"));
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Delete_IdIsNull_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Delete(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_PersonsIsNull_ReturnBadRequestResult()
        {
            // Arrange

            // Act
            var result = await controller.Delete(100);

            // Arrange
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task DeleteConfirmed_ReturnRedirectToAction()
        {
            // Arrange

            //Act
            var result = await controller.DeleteConfirmed(100);

            //Assert
            persons.Verify(m => m.DeletePersonsAsync(100));
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }

    }
}
