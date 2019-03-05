using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    /// <summary>
    /// Интерфейс работы с пользователями
    /// </summary>
    public interface IUserDataService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="pass">Пароль</param>
        Task<IdentityResult> Register(UserData user, string pass);

        /// <summary>
        /// Вход на сайт при регистрации
        /// </summary>
        /// <param name="user">Пользователь</param>
        Task SignIn(UserData user);

        /// <summary>
        /// Вход на сайт по паролю
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="pass">Пароль</param>
        /// <param name="rememberMe">Флаг запоминания на сайте</param>
        Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignIn(string email, string pass, bool rememberMe);
    }
}

