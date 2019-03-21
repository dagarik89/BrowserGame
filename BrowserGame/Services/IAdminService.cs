using BrowserGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserGame.Services
{
    /// <summary>
    /// Интерфейс работы администратора
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Получает логи
        /// </summary>
        LogsViewModel GetLogs();

        /// <summary>
        /// Получает логи по дате
        /// </summary>
        /// <param name="logs">Логи</param>
        LogsViewModel GetLogsByDate(LogsViewModel logs);
    }
}
