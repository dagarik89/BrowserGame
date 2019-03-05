using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель регистрации
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [RegularExpression(@"^(?=.*[a-z])[A-Za-z\d@$!%^*?=#/&-_+`{|}~]{6,12}$", ErrorMessage = "{0} должен содержать хотя бы одну букву ('a'-'z'), не содержать русские буквы, быть не менее 6 и не более 12 символов.")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
