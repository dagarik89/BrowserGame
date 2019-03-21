using BrowserGame.Controllers;
using BrowserGame.Services;
using BrowserGame.ViewModels;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        public class FakeUserManager : UserManager<UserData>
        {
            public FakeUserManager()
                : base(new Mock<IUserStore<UserData>>().Object,
                      new Mock<IOptions<IdentityOptions>>().Object,
                      new Mock<IPasswordHasher<UserData>>().Object,
                      new IUserValidator<UserData>[0],
                      new IPasswordValidator<UserData>[0],
                      new Mock<ILookupNormalizer>().Object,
                      new Mock<IdentityErrorDescriber>().Object,
                      new Mock<IServiceProvider>().Object,
                      new Mock<ILogger<UserManager<UserData>>>().Object)
            { }
        }

        public class FakeSignInManager : SignInManager<UserData>
        {
            public FakeSignInManager()
                : base(new FakeUserManager(),
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<UserData>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<UserData>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object)
            { }
        }

        AccountController controller;
        Mock<ILogger<AccountController>> log;
        Mock<IUserService> user;
        Mock<FakeUserManager> _userManagerMock;
        Mock<FakeSignInManager> _signInManagerMock;
        LoginViewModel loginModel;
        UserData userData;

        [SetUp]
        public void Setup()
        {
            // Arrange
            log = new Mock<ILogger<AccountController>>();
            user = new Mock<IUserService>();
            _userManagerMock = new Mock<FakeUserManager>();
            _signInManagerMock = new Mock<FakeSignInManager>();

            loginModel = new LoginViewModel
            {
                Email = "SomeEmail",
                Password = "SomePassword"
            };

            userData = new UserData
            {
                Id = "1"
            };

            controller = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);
        }

        [Test]
        public async Task CanLoginWithValidCredentials()
        {
            // Arrange
            _userManagerMock.Setup(m => m.FindByNameAsync(loginModel.Email)).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.IsEmailConfirmedAsync(userData)).ReturnsAsync(true);

            _signInManagerMock.Setup(m => m.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            AccountController contr = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);

            // Act
            var result = await contr.Login(loginModel);

            // Assert
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
        }

        [Test]
        public async Task ConfrimReturnsRedirectToActionResultWithValidModel()
        {
            // Arrange
            _userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.ConfirmEmailAsync(userData, "token")).ReturnsAsync(IdentityResult.Success);

            AccountController contr = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);

            // Act
            var result = await contr.ConfirmEmail("userId", "token");

            // Assert
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
        }

        [Test]
        public async Task ConfrimReturnsErrorWithInvalidModel()
        {
            // Arrange
            _userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.ConfirmEmailAsync(userData, "token")).ReturnsAsync(IdentityResult.Failed());

            AccountController contr = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);

            // Act
            var result = await contr.ConfirmEmail("userId", "token");

            // Assert
            Assert.AreEqual("Error", (result as ViewResult).ViewName);
        }

        [Test]
        public async Task LoginReturnsErrorWithEmailNotConfirmed()
        {
            // Arrange
            _userManagerMock.Setup(m => m.FindByNameAsync(loginModel.Email)).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.IsEmailConfirmedAsync(userData)).ReturnsAsync(false);

            AccountController contr = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);

            //Act
            var result = await contr.Login(loginModel);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual(contr.ModelState
                .Values
                .First()
                .Errors[0]
                .ErrorMessage, "Вы не подтвердили свой email");
        }

        [Test]
        public async Task LoginReturnsErrorWithWrongLoginOrPassword()
        {
            // Arrange
            _userManagerMock.Setup(m => m.FindByNameAsync(loginModel.Email)).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.IsEmailConfirmedAsync(userData)).ReturnsAsync(true);

            _signInManagerMock.Setup(m => m.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            AccountController contr = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);

            //Act
            var result = await contr.Login(loginModel);

            //Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual(contr.ModelState
                .Values
                .First()
                .Errors[0]
                .ErrorMessage, "Неправильный логин и (или) пароль");
        }

        [Test]
        public async Task ResetPasswordReturnsViewResultWithValidModel()
        {
            // Arrange
            ResetPasswordViewModel resetPasswordModel = new ResetPasswordViewModel
            {
                Email = "SomeEmail",
                Password = "SomePassword"
            };

            _userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.ResetPasswordAsync(userData, "token", "newPassword")).ReturnsAsync(IdentityResult.Success);

            AccountController contr = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, user.Object, log.Object);

            // Act
            var result = await contr.ResetPassword(resetPasswordModel);

            // Assert
            Assert.AreEqual("ResetPasswordConfirmation", (result as ViewResult).ViewName);
        }

    }
}
