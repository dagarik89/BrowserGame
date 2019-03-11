using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.ViewModels
{
    /// <summary>
    /// Модель игры
    /// </summary>
    public class GameViewModel
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
    }
}
