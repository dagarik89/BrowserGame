using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Интерфейс работы с персонажами
    /// </summary>
    public interface IPersonsBusinessService
    {
        /// <summary>
        /// Получает список персонажей
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        Task<IList<PersonsBusiness>> GetPersons(string name);

        /// <summary>
        /// Удаление персонажа
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        Task DeletePersonsAsync(int id);

        /// <summary>
        /// Получает персонаж по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        Task<PersonsBusiness> GetDetails(int id);

        /// <summary>
        /// Создает/изменяет персонаж
        /// </summary>
        /// <param name="persons">Персонаж</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="operation">Операция (создание/изменение)</param>
        Task CreatePers(PersonsBusiness persons, string name, string operation);

        /// <summary>
        /// Получает список персонажей с именем, идентичным параметру
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        /// <param name="operation">Операция (создание/изменение)</param>
        IList<PersonsBusiness> EqualPers(string name, string operation, int? id);

        /// <summary>
        /// Получает игровые характеристики персонажа
        /// </summary>
        /// <param name="persons">Персонаж</param>
        GameModel GetGame(PersonsBusiness persons);

        /// <summary>
        /// Сохраняет игровой результат
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// /// <param name="model">Модель игры</param>
        Task SaveMaxResult(string name, int id, int maxPoints);
    }
}