using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    /// <summary>
    /// Интерфейс работы с персонажами
    /// </summary>
    public interface IPersonsDataService
    {
        /// <summary>
        /// Получает список персонажей
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        Task<IList<PersonsData>> GetPersons(string name);

        /// <summary>
        /// Удаление персонажа из БД
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Получает персонаж по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        Task<PersonsData> GetDetails(int id);

        /// <summary>
        /// Создает персонаж в БД
        /// </summary>
        /// <param name="persons">Персонаж</param>
        /// <param name="name">Имя пользователя</param>
        Task CreatePers(PersonsData persons, string name);

        /// <summary>
        /// Изменяет персонаж в БД
        /// </summary>
        /// <param name="persons">Персонаж</param>
        /// <param name="name">Имя пользователя</param>
        Task UpdatePers(PersonsData persons, string name);

        /// <summary>
        /// Получает список персонажей с именем, идентичным параметру
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        IList<PersonsData> EqualPers(string name);

        /// <summary>
        /// Получает список персонажей с именем, идентичным параметру
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        /// <param name="id">Идентификатор персонажа</param>
        IList<PersonsData> EqualPersUpdate(string name, int id);

    }
}
