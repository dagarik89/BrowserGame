using BusinessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Интерфейс работы с пользователями
    /// </summary>
    public interface IUserBusinessService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="pass">Пароль</param>
        Task<IdentityResult> Register(UserBusiness user, string pass);

        /// <summary>
        /// Вход на сайт при регистрации
        /// </summary>
        /// <param name="user">Пользователь</param>
        Task SignIn(UserBusiness user);

        /// <summary>
        /// Вход на сайт по паролю
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="pass">Пароль</param>
        /// <param name="rememberMe">Флаг запоминания на сайте</param>
        Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignIn(string email, string pass, bool rememberMe);

        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="callbackUrl">Строка callback</param>
        Task ForgotPassword(string email, string callbackUrl);

        /// <summary>
        /// Подтверждение регистрации
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="callbackUrl">Строка callback</param>
        Task RegistrationConfirm(string email, string callbackUrl);
    }
}
