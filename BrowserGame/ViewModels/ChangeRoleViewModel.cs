using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель изменения ролей
    /// </summary>
    public class ChangeRoleViewModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Список всех ролей
        /// </summary>
        public List<IdentityRole> AllRoles { get; set; }

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public IList<string> UserRoles { get; set; }

        /// <summary>
        /// Конструктор модели изменения ролей
        /// </summary>
        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
