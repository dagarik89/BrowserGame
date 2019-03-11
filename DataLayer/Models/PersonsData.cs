using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    /// <summary>
    /// Персонаж
    /// </summary>
    public class PersonsData
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public int PersonsID { get; set; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цветовая схема
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Размер
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Рекорд
        /// </summary>
        public int MaxPoints { get; set; }

        /// <summary>
        /// Попытки
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }
    }
}
