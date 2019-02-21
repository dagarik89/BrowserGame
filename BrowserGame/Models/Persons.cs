using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Models
{
    public class Persons
    {
        public int PersonsID { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [RegularExpression(@"[а-яa-zA-ZА-Я0-9]*", ErrorMessage = "Некорректное имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Цветовая схема")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Range(10, 50, ErrorMessage = "Диапазон от 10 до 50")]
        [Display(Name = "Скорость")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Range(10, 30, ErrorMessage = "Диапазон от 10 до 30")]
        [Display(Name = "Размер")]
        public int Size { get; set; }

        [Display(Name = "Рекорд")]
        public int MaxPoints { get; set; }

        [Display(Name = "Попытки")]
        public int Attempts { get; set; }

        [Display(Name = "Пользователь")]
        public string User { get; set; }
    }
}
