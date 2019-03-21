using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Интерфейс работы администратора
    /// </summary>
    public interface IAdminBusinessService
    {
        /// <summary>
        /// Получает логи
        /// </summary>
        LogsModel GetLogs();

        /// <summary>
        /// Получает логи по дате
        /// </summary>
        /// <param name="logs">Логи</param>
        LogsModel GetLogsByDate(LogsModel logs);
    }
}
