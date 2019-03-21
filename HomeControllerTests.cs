using BrowserGame.Controllers;
using BrowserGame.Services;
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
        /*HomeController controller;
        [SetUp]
        public void Setup()
        {
            var log = new Mock<ILogger<HomeController>>();
            var admin = new Mock<IAdminService>();
             controller = new HomeController(log.Object, admin.Object);
        }

        [Test]
        public void IndexViewNameEqualIndex()
        {
            var result = controller.Index() as ViewResult;

            Assert.Equals("Index", result?.ViewName);
            //Assert.Pass();
        }

        [Test]
        public void IndexViewResultNotNull()
        {
            // Arrange
            //HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }*/

        [Test]
        public void Test1()
        {
            // Arrange
            //HomeController controller = new HomeController();
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.AreEqual(5, 2+3);
        }
    }
}
