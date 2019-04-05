using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    /// <summary>
    /// Модель игры
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// Цвет змейки
        /// </summary>
        public string Snake_color { get; set; }

        /// <summary>
        /// цвет еды
        /// </summary>
        public string Food_color { get; set; }

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
        /// Идентификатор персонажа
        /// </summary>
        public int PersonID { get; set; }
    }
}
