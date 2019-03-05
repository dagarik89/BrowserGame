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

namespace BrowserGame.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;


        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Game()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Logs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error(int? id)
        {
            _logger.LogWarning("Ошибка({ID}) в {RequestTime}", id, DateTime.Now);
            return Redirect($"~/{id}.htm");
        }

    }
}
