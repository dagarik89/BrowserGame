using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Models
{
    /// <summary>
    /// Персонаж
    /// </summary>
    public class Persons
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public int PersonsID { get; set; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [RegularExpression(@"[а-яa-zA-ZА-Я0-9]*", ErrorMessage = "Некорректное имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Цветовая схема
        /// </summary>
        [Display(Name = "Цветовая схема")]
        public string Color { get; set; }

        /// <summary>
        /// Скорость
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Range(10, 50, ErrorMessage = "Диапазон от 10 до 50")]
        [Display(Name = "Скорость")]
        public int Speed { get; set; }

        /// <summary>
        /// Размер
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Range(10, 30, ErrorMessage = "Диапазон от 10 до 30")]
        [Display(Name = "Размер")]
        public int Size { get; set; }

        /// <summary>
        /// Рекорд
        /// </summary>
        [Display(Name = "Рекорд")]
        public int MaxPoints { get; set; }

        /// <summary>
        /// Попытки
        /// </summary>
        [Display(Name = "Попытки")]
        public int Attempts { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string User { get; set; }
    }
}
