using EducationApp.Models;
using EducationApp.Models.Data;
using EducationApp.ViewModels.AccountViewModels;
using EducationApp.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Controllers
{
    public class UserController : Controller
    {
            private readonly AppCtx _context;
            public UserController/*(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)*/
            (AppCtx context)
            {
                _context = context;
            }

            // отображение списка пользователей
            // действия для начальной страницы Index
            public IActionResult Index() => View(_context.Users.ToList());


            // действия для создания пользователя Create
            public IActionResult Create() => View();


            [HttpPost]
            public IActionResult Create(RegisterViewModels model)
            {
                if (ModelState.IsValid)
                {
                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.FirstName,
                        LastName = model.LastName,
                        FirstName = model.FirstName,
                        Patronymic = model.Patronymic
                    };

                    return RedirectToAction("Index");
                }
                return View(model);
            }
            // GET: User/Delete/5
            public async Task<IActionResult> Delete(string id)
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
                if (users == null)
                {
                    return NotFound();
                }

                return View(users);
            }
            // POST: User/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(string id)
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'AppCtx.Users'  is null.");
                }
                var users = await _context.Users.FindAsync(id);
                if (users != null)
                {
                    _context.Users.Remove(users);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
        /*
            // действия для изменения пользователя Edit
            public async Task<IActionResult> Edit(string id)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                EditUserViewModel model = new EditUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Patronymic = user.Patronymic
                };
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(EditUserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        user.LastName = model.LastName;
                        user.FirstName = model.FirstName;
                        user.Patronymic = model.Patronymic;

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

        
            // действия для удаления пользователя Delete с подтверждением
            // GET: Users/Delete/5
            public async Task<ActionResult> Delete(string id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                User user = await _context.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }

            // POST: Users/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(string id)
            {
                User user = await _userManager.FindByIdAsync(id);
                IdentityResult result = await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> ChangePassword(string id)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                ChangePasswordViewModel model = new ChangePasswordViewModel
                {
                    Id = user.Id,
                    Email = user.Email
                };
                return View(model);
            }
        
            [HttpPost]
            public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        var _passwordValidator =
                            HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                        var _passwordHasher =
                            HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                        IdentityResult result =
                            await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                        if (result.Succeeded)
                        {
                            user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                            await _userManager.UpdateAsync(user);
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

            // так как роли задаются для каждого пользователя системы отдельно,
            // то можно перенести методы работы с ними в контроллер Users, где мы можеи получить доступ ко всем пользователям системы
            public async Task<IActionResult> EditRoles(string userId)
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserRoles = userRoles,
                        AllRoles = allRoles
                    };
                    return View(model);
                }

                return NotFound();
            }

            [HttpPost]
            public async Task<IActionResult> EditRoles(string userId, List<string> roles)
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    // получаем все роли
                    var allRoles = _roleManager.Roles.ToList();
                    // получаем список ролей, которые были добавлены
                    var addedRoles = roles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    return RedirectToAction(nameof(Index));
                }

                return NotFound();
            }
        }*/
    }
}
