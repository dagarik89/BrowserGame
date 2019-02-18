using System.ComponentModel.DataAnnotations;

namespace BrowserGame.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
