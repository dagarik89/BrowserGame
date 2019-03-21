using BrowserGame.Controllers;
using BrowserGame.Services;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BrowserGame.Tests.AccountControllerTests;

namespace BrowserGame.Tests
{
    [TestFixture]
    public class RolesControllerTests
    {
        public class FakeRoleManager : RoleManager<IdentityRole>
        {
            public FakeRoleManager()
                : base(new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object)
            { }
        }

        RolesController controller;
        Mock<ILogger<RolesController>> log;
        Mock<FakeUserManager> _userManagerMock;
        Mock<FakeRoleManager> _roleManagerMock;

        [SetUp]
        public void Setup()
        {
            // Arrange
            log = new Mock<ILogger<RolesController>>();
            _userManagerMock = new Mock<FakeUserManager>();
            _roleManagerMock = new Mock<FakeRoleManager>();

            controller = new RolesController(_roleManagerMock.Object, _userManagerMock.Object);
        }

        [Test]
        public async Task EditReturnsRedirectToActionResult()
        {
            // Arrange
            List<string> listOfRoles = new List<string> { };

            UserData userData = new UserData
            {
                Id = "1"
            };

            _userManagerMock.Setup(m => m.FindByIdAsync("name")).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.GetRolesAsync(userData)).ReturnsAsync(listOfRoles);

            RolesController contr = new RolesController(_roleManagerMock.Object, _userManagerMock.Object);

            // Act
            var result = await contr.Edit("name", listOfRoles);

            // Assert
            Assert.AreEqual("UserList", (result as RedirectToActionResult).ActionName);
        }

        [Test]
        public async Task DeleteReturnsRedirectToActionResult()
        {
            // Arrange
            IdentityRole role = new IdentityRole { Id = "roleId"};

            _roleManagerMock.Setup(m => m.FindByIdAsync("role")).ReturnsAsync(role);

            RolesController contr = new RolesController(_roleManagerMock.Object, _userManagerMock.Object);

            // Act
            var result = await contr.Delete("role");

            // Assert
            _roleManagerMock.Verify(m => m.DeleteAsync(role));
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
        }

    }
}
