using BrowserGame.Models;
using BrowserGame.ViewModels;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUserBusinessService userData;

        public UserService(IUserBusinessService userData)
        {
            this.userData = userData;
        }

        public async Task<IdentityResult> Register(User user, string pass)
        {
            var baseUser = user.Adapt<UserBusiness>();
            return (await this.userData.Register(baseUser, pass)).Adapt<IdentityResult>();
        }

        public Task SignIn(User user)
        {
            var baseUser = user.Adapt<UserBusiness>();
            return userData.SignIn(baseUser);
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignIn(string email, string pass, bool rememberMe)
        {
            return (await this.userData.PasswordSignIn(email, pass, rememberMe)).Adapt<Microsoft.AspNetCore.Identity.SignInResult>();
        }

        public Task ForgotPassword(string email, string callbackUrl)
        {
            return userData.ForgotPassword(email, callbackUrl);
        }

        public Task RegistrationConfirm(string email, string callbackUrl)
        {
            return userData.RegistrationConfirm(email, callbackUrl);
        }

    }
}
