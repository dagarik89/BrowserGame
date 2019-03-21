using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrowserGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using DataLayer.Data;
using System.IO;
using BrowserGame.ViewModels;
using BrowserGame.Services;

namespace BrowserGame.Controllers
{
    /// <summary>
    /// Базовый контроллер приложения
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IAdminService _admin;

        public HomeController(ILogger<HomeController> logger, IAdminService admin)
        {
            //_context = context;
            _logger = logger;
            _admin = admin;
        }

        /// <summary>
        /// Получает главную страницу
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Получает страницу конфиденциальности
        /// </summary>
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Получает страницу игры
        /// </summary>
        [HttpGet]
        public IActionResult Game()
        {
            return View();
        }

        /// <summary>
        /// Получает страницу логов
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Logs()
        {
            LogsViewModel model = _admin.GetLogs();
            return View(model);
        }

        /// <summary>
        /// Получает страницу логов по дате
        /// </summary>
        /// <param name="logs">Модель логов</param>
        [HttpPost]
        public IActionResult Logs(LogsViewModel logs)
        {
            if (ModelState.IsValid)
            {
                LogsViewModel model = _admin.GetLogsByDate(logs);
                return View(model);
            }

            return View(new LogsViewModel {Date = null, Text = "За указанную дату нет логов!"});
        }

        /// <summary>
        /// Обработка ошибок http
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error(int? id)
        {
            _logger.LogWarning("Ошибка({ID}) в {RequestTime}", id, DateTime.Now);
            return Redirect($"~/{id}.htm");
        }

    }
}
