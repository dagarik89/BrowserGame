using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    /// <summary>
    /// Модель отображения логов
    /// </summary>
    public class LogsModel
    {
        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Текст логов
        /// </summary>
        public string Text { get; set; }
    }
}