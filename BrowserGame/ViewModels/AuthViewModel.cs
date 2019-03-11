using BrowserGame.Models;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель аутентификации
    /// </summary>
    public class AuthViewModel : PageModel
    {
        /// <summary>
        /// Сервис для аутентификации
        /// </summary>
        private readonly SignInManager<UserData> _signInManager;

        /// <summary>
        /// Конструктор модели аутентификации
        /// </summary>
        public AuthViewModel(SignInManager<UserData> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Строка возврата
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Список внешних служб проверки подлинности
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Получение списка внешних служб проверки подлинности
        /// </summary>
        /// <param name="returnUrl">Строка возврата</param>
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
    }
}
