using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using BrowserGame.ViewModels;
using BrowserGame.Services;

namespace BrowserGame.Controllers
{
    /// <summary>
    /// Класс управления персонажами
    /// </summary>
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IPersonsService _pers;

        public PersonsController(ILogger<PersonsController> logger, IPersonsService pers)
        {
            _logger = logger;
            _pers = pers;
        }

        // GET: Persons
        /// <summary>
        /// Получает список персонажей
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await this._pers.GetPersons(HttpContext.User.Identity.Name));
        }

        // GET: Persons/Details/5
        /// <summary>
        /// Получает данные о персонаже
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _pers.GetDetails((int)id);

            if (persons == null)
            {
                return BadRequest();
            }

            if (persons.User != HttpContext.User.Identity.Name || persons.User == "Default")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(persons);
        }

        // GET: Game
        /// <summary>
        /// Получает игровую модель
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        [HttpGet]
        public async Task<IActionResult> Game(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _pers.GetDetails((int)id);

            if (persons == null)
            {
                return BadRequest();
            }
            
            if (persons.User != HttpContext.User.Identity.Name && persons.User != "Default")
            {
                return RedirectToAction(nameof(Index));
            } 

            GameViewModel model = _pers.GetGame(persons);

            return View(model);
        }

        // GET: Persons/Create
        /// <summary>
        /// Получает страницу создания персонажей
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        /// <summary>
        /// Создает персонаж
        /// </summary>
        /// <param name="persons">Модель персонажа</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persons persons)
        {
            if (persons.Name.Length < 3 || persons.Name.Length > 10)
            {
                ModelState.AddModelError("Name", "Длина строки должна быть от 3 до 10 символов");
            }

            if (_pers.EqualPers(persons.Name, "add", null).Count() > 0)
            {
                ModelState.AddModelError("Name", "Персонаж с таким именем занят!");
            }

            if (ModelState.IsValid)
            {
                var name = HttpContext.User.Identity.Name;

                var persId = await _pers.CreatePers(persons, name, "add");
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        // GET: Persons/Edit/5
        /// <summary>
        /// Получает страницу редактирования персонажа
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _pers.GetDetails((int)id);

            if (persons == null)
            {
                return BadRequest();
            }

            if (persons.User != HttpContext.User.Identity.Name || persons.User == "Default")
            {
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        // POST: Persons/Edit/5
        /// <summary>
        /// Редактирует данные персонажа
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        /// <param name="persons">Модель персонажа</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persons persons)
        {
            if (id != persons.PersonsID)
            {
                return NotFound();
            }

            if (_pers.EqualPers(persons.Name, "update", persons.PersonsID).Count() > 0)
            {
                ModelState.AddModelError("Name", "Персонаж с таким именем занят!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pers.CreatePers(persons, HttpContext.User.Identity.Name, "update");
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        // GET: Persons/Delete/5
        /// <summary>
        /// Получает страницу удаления персонажа
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _pers.GetDetails((int)id);

            if (persons == null)
            {
                return BadRequest();
            }

            if (persons.User != HttpContext.User.Identity.Name || persons.User == "Default")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(persons);
        }

        // POST: Persons/Delete/5
        /// <summary>
        /// Удаляет персонаж
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pers.DeletePersonsAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
