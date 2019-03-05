using System.ComponentModel.DataAnnotations;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель сброса пароля
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
