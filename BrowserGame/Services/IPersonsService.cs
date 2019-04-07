using BrowserGame.Models;
using BrowserGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Services
{
    /// <summary>
    /// Интерфейс работы с персонажами
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Получает список персонажей
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        Task<IEnumerable<Persons>> GetPersons(string name);

        /// <summary>
        /// Создает/изменяет персонаж
        /// </summary>
        /// <param name="persons">Персонаж</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="operation">Операция (создание/изменение)</param>
        Task<int> CreatePers(Persons persons, string name, string operation);

        /// <summary>
        /// Получает список персонажей с именем, идентичным параметру
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        /// <param name="operation">Операция (создание/изменение)</param>
        IEnumerable<Persons> EqualPers(string name, string operation, int? id);

        /// <summary>
        /// Удаление персонажа
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        Task DeletePersonsAsync(int id);

        /// <summary>
        /// Получает персонаж по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        Task<Persons> GetDetails(int id);

        /// <summary>
        /// Получает игровые характеристики персонажа
        /// </summary>
        /// <param name="persons">Персонаж</param>
        GameViewModel GetGame(Persons persons);

        /// <summary>
        /// Сохраняет игровой результат
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// /// <param name="model">Модель игры</param>
        Task SaveMaxResult(string name, int id, int maxPoints);
    }
}