using BusinessLayer.Models;
using BusinessLayer.Services;
using DataLayer.Models;
using DataLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementation
{
    class UserBusinessService : IUserBusinessService
    {
        private readonly IUserDataService userServices;

        public UserBusinessService(IUserDataService userServices)
        {
            this.userServices = userServices;
        }

        public async Task<IdentityResult> Register(UserBusiness user, string pass)
        {
            var userData = user.Adapt<UserData>();
            return await this.userServices.Register(userData, pass);
        }

        public Task SignIn(UserBusiness user)
        {
            var userData = user.Adapt<UserData>();
            return userServices.SignIn(userData);
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignIn(string email, string pass, bool rememberMe)
        {
            return await this.userServices.PasswordSignIn(email, pass, rememberMe);
        }

        public async Task ForgotPassword(string email, string callbackUrl)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(email, "Сброс пароля",
                $"Для сброса пароля пройдите по <a href='{callbackUrl}'>ссылке</a>");
        }

        public async Task RegistrationConfirm(string email, string callbackUrl)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(email, "Подтвердите свой аккаунт",
                $"Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>");
        }

    }
}
