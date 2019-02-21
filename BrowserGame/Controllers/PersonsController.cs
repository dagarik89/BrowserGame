using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Data;
using BrowserGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using BrowserGame.ViewModels;

namespace BrowserGame.Controllers
{
    /// <summary>
    /// Класс управления персонажами
    /// </summary>
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PersonsController(ApplicationDbContext context, ILogger<PersonsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Persons
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userPersons = _context.Persons
                .Where(m => m.User == HttpContext.User.Identity.Name || m.User == "Default");

            return View(await userPersons.ToListAsync());
        }

        // GET: Persons/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonsID == id);
            if (persons == null)
            {
                return NotFound();
            }

            if (persons.User != HttpContext.User.Identity.Name || persons.User == "Default")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(persons);
        }


        // GET: Game
        [HttpGet]
        public async Task<IActionResult> Game(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonsID == id);
            if (persons == null)
            {
                return NotFound();
            }
            
            if (persons.User != HttpContext.User.Identity.Name && persons.User != "Default")
            {
                return RedirectToAction(nameof(Index));
            }

            string snake_color, food_color;

            switch (persons.Color)
            {
                case "Чёрный+красный":
                    snake_color = "#000";
                    food_color = "red";
                    break;
                case "Зелёный+красный":
                    snake_color = "green";
                    food_color = "red";
                    break;
                case "Синий+красный":
                    snake_color = "blue";
                    food_color = "red";
                    break;
                case "Чёрный+зелёный":
                    snake_color = "#000";
                    food_color = "green";
                    break;
                case "Синий+зелёный":
                    snake_color = "blue";
                    food_color = "green";
                    break;
                case "Красный+зелёный":
                    snake_color = "red";
                    food_color = "green";
                    break;
                default:
                    snake_color = "#000";
                    food_color = "red";
                    break;
            }

            GameViewModel model = new GameViewModel
            {
                Speed = persons.Speed,
                Size = persons.Size,
                Snake_color = snake_color,
                Food_color = food_color
            };

            return View(model);
        }



        // GET: Persons/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persons persons)
        {
            if (persons.Name.Length < 3 || persons.Name.Length > 10)
            {
                ModelState.AddModelError("Name", "Длина строки должна быть от 3 до 10 символов");
            }

            var equalPersons = _context.Persons
                .Where(m => m.Name == persons.Name);

            if (equalPersons.Count() > 0)
            {
                ModelState.AddModelError("Name", "Персонаж с таким именем занят!");
            }

            if (ModelState.IsValid)
            {
                persons.User = HttpContext.User.Identity.Name;
                _context.Add(persons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        // GET: Persons/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons.FindAsync(id);
            if (persons == null)
            {
                return NotFound();
            }

            if (persons.User != HttpContext.User.Identity.Name || persons.User == "Default")
            {
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persons persons)
        {
            if (id != persons.PersonsID)
            {
                return NotFound();
            }

            var equalPersons = _context.Persons
                .Where(m => m.Name == persons.Name && m.PersonsID != persons.PersonsID);

            if (equalPersons.Count() > 0)
            {
                ModelState.AddModelError("Name", "Персонаж с таким именем занят!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonsExists(persons.PersonsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        // GET: Persons/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonsID == id);
            if (persons == null)
            {
                return NotFound();
            }

            if (persons.User != HttpContext.User.Identity.Name || persons.User == "Default")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(persons);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persons = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(persons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.PersonsID == id);
        }
    }
}
