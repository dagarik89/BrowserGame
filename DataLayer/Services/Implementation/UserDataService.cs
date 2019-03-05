using DataLayer.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services.Implementation
{
    internal class UserDataService : IUserDataService
    {
        private ApplicationDbContext db;
        private UserManager<UserData> _userManager;
        private SignInManager<UserData> _signInManager;
        public UserDataService(ApplicationDbContext context, UserManager<UserData> userManager, SignInManager<UserData> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(UserData user, string pass)
        {
            //UserData userData = new UserData { Email = user.Email, UserName = user.Email };
                // добавляем пользователя

                return await _userManager.CreateAsync(user, pass);
            //await this.db.SaveChangesAsync();
        }

        public Task SignIn(UserData user)
        {
            return _signInManager.SignInAsync(user, false);
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignIn(string email, string pass, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(email);
            return await _signInManager.PasswordSignInAsync(email,pass,rememberMe,false);
        }
    }
}
