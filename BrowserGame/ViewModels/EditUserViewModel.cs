using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель изменения пользователей
    /// </summary>
    public class EditUserViewModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
