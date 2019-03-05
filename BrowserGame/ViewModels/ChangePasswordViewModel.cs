using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель изменения пароля
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Старый пароль
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string OldPassword { get; set; }
    }
}
