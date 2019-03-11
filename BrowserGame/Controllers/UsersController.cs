using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrowserGame.Models;
using BrowserGame.ViewModels;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrowserGame.Controllers
{
    /// <summary>
    /// Класс управления пользователями
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        UserManager<UserData> _userManager;

        public UsersController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Получает список пользователей
        /// </summary>
        [HttpGet]
        public IActionResult Index() => View(_userManager.Users.ToList());

        /// <summary>
        /// Получает страницу добавления пользователя
        /// </summary>
        [HttpGet]
        public IActionResult Create() => View();

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserData user = new UserData { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Получает страницу редактирования данных пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserData user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        /// <summary>
        /// Редактирование данных пользователя
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserData user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Получает страницу удаления записи о пользователе
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            UserData user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Удаление записи о пользователе
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            UserData user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        /// <summary>
        /// Смена пароля
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserData user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}