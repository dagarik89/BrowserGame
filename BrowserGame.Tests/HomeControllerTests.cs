using BrowserGame.Controllers;
using BrowserGame.Services;
using BrowserGame.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserGame.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        HomeController controller;
        Mock<ILogger<HomeController>> log;
        Mock<IAdminService> admin;

        [SetUp]
        public void Setup()
        {
            // Arrange
            log = new Mock<ILogger<HomeController>>();
            admin = new Mock<IAdminService>();
            controller = new HomeController(log.Object, admin.Object);
        }

        [Test]
        public void IndexViewResultIsViewResultObject()
        {
            // Arrange

            // Act
            var result = controller.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void IndexViewNameEqualIndex()
        {
            // Arrange

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Index", (result as ViewResult).ViewName);
        }

        [Test]
        public void IndexViewResultNotNull()
        {
            // Arrange

            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result as ViewResult);
        }

        [Test]
        public void CanViewLogs()
        {
            // Arrange
            admin.Setup(m => m.GetLogs()).Returns(
                new LogsViewModel { Date = "2019-01-01", Text = "Some logs"}     
                );
            HomeController contr = new HomeController(log.Object, admin.Object);

            //Act
            var result = contr.Logs();
        
            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.TypeOf<LogsViewModel>());
            Assert.AreEqual("Some logs", ((result as ViewResult).Model as LogsViewModel).Text);
        }

        [Test]
        public void CanViewLogsByDate()
        {
            // Arrange
            LogsViewModel model = new LogsViewModel { Date = "2019-01-01" };

            admin.Setup(m => m.GetLogsByDate(model)).Returns(
                new LogsViewModel { Date = "2019-01-01", Text = "Some logs" }
                );
            HomeController contr = new HomeController(log.Object, admin.Object);

            //Act
            var result = contr.Logs(model);

            //Assert
            Assert.That((result as ViewResult).Model, Is.TypeOf<LogsViewModel>());
            Assert.AreEqual("Some logs", ((result as ViewResult).Model as LogsViewModel).Text);
        }

        [Test]
        public void CanViewLogsByDateVerify()
        {
            // Arrange
            LogsViewModel model = new LogsViewModel { Date = "2019-01-01" };

            //Act
            var result = controller.Logs(model);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            admin.Verify(m => m.GetLogsByDate(model));
        }

        [Test]
        public void CanViewLogsByDateWithInvalidModel()
        {
            // Arrange
            //Создание не валидной модели
            LogsViewModel modelInvalid = new LogsViewModel();

            controller.ModelState.AddModelError("Date", "Required");

            //Act
            var result = controller.Logs(modelInvalid);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual("За указанную дату нет логов!", ((result as ViewResult).Model as LogsViewModel).Text);
        }

        [Test]
        public void ErrorReturnsARedirect()
        {
            // Arrange

            //Act
            var result = controller.Error(404);

            //Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
            Assert.AreEqual((result as RedirectResult).Url, $"~/404.htm");
        }

    }
}
