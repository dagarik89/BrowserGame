using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Services.Implementation;
using BusinessLayer.Services;
using BusinessLayer.Models;

namespace BusinessLayer.Tests
{
    [TestFixture]
    public class AdminBusinessServiceTests
    {
        [Test]
        public void GetLogsByDateReturnsLogsModel()
        {
            // Arrange
            var adminService = new AdminBusinessService();

            // Act
            var result = adminService.GetLogsByDate(new LogsModel { Date = "2019-01-01", Text = "SomeText"});

            // Assert
            Assert.AreEqual("01.01.2019", result.Date);
            Assert.That(result, Is.TypeOf<LogsModel>());
        }
    }
}
