using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель отображения логов
    /// </summary>
    public class LogsViewModel
    {
        /// <summary>
        /// Дата
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Date { get; set; }

        /// <summary>
        /// Текст логов
        /// </summary>
        public string Text { get; set; }
    }
}