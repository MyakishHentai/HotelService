using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HotelService.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;

namespace HotelService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Developer")]
    public class UserController : Controller
    {
        private readonly HotelServiceContext m_Context;
        private RoleManager<Role> m_RoleManager;
        private UserManager<User> m_UserManager;

        public UserController(UserManager<User> usrMgr, RoleManager<Role> roleMgr, HotelServiceContext context)
        {
            m_UserManager = usrMgr;
            m_RoleManager = roleMgr;
            m_Context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await m_UserManager.Users.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateEdit(string id)
        {
            // TODO: Добавить пол
            ViewBag.Genders = new SelectList(new List<string> { "Male", "Female" });

            if (id == null)
            {

                return View(new CreateModel());
            }

            var User = await m_UserManager.FindByIdAsync(id);
            if (User != null) return View(new CreateModel
            {
                Id = User.Id,
                UserName = User.UserName,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Patronymic = User.Patronymic,
                BirthDate = User.BirthDate,
                Passport = User.Passport,
                Gender = User.Gender,
                Email = User.Email
            });
            return NotFound();
        }


        public async Task<IdentityResult> PasswordValidator(User user,string password)
        {
            var PasswordValidator =
                HttpContext.RequestServices.GetService(
                    typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;

            var ValidateResult = await PasswordValidator.ValidateAsync(m_UserManager, user, password);
            return ValidateResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(CreateModel input, string id)
        {
            if (!ModelState.IsValid)
                return Json(new
                    { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEdit", input) });

            var PasswordHasher =
                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
            //Insert
            if (id == null)
            {
                var NewUser = new User
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Patronymic = input.Patronymic,
                    BirthDate = input.BirthDate,
                    Passport = input.Passport,
                    Gender = input.Gender,
                    Email = input.Email,
                    UserName = input.Email
                };
                var Result = await m_UserManager.CreateAsync(NewUser, input.Password);
                if (Result.Succeeded)
                {
                    return Json(new
                    {
                        isValid = true,
                        html = HelperView.RenderRazorViewToString(this, "_ViewTable", m_UserManager.Users)
                    });
                }
                AddErrorsFromResult(Result);
                return Json(new
                    {isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEdit", input)});
            }
            // TODO: Нужно ли обновлять пароль - всегда обновляет пароль
            //Update
            var UpdateUser = await m_UserManager.FindByIdAsync(id);
            var ValidateResult = await PasswordValidator(UpdateUser, input.Password);
            if (ValidateResult.Succeeded)
            {
                UpdateUser.PasswordHash = PasswordHasher.HashPassword(UpdateUser, input.Password);
                await m_UserManager.UpdateAsync(UpdateUser);
                return Json(new
                {
                    isValid = true,
                    html = HelperView.RenderRazorViewToString(this, "_ViewTable", m_UserManager.Users)
                });
            }
            AddErrorsFromResult(ValidateResult);
            return Json(new
                { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEdit", input) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Details(string id)
        {
            var User = await m_UserManager.FindByIdAsync(id);
            if (User != null) return View(User);
            return NotFound();
        }


        [HttpGet]
        [NoDirectAccess]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var UserNow = await m_UserManager.FindByIdAsync(id);
            if (UserNow != null) return View(UserNow);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var User = await m_UserManager.FindByIdAsync(id);
            if (User != null)
            {
                var Result = await m_UserManager.DeleteAsync(User);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                AddErrorsFromResult(Result);
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", m_UserManager.Users);
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var Error in result.Errors)
            {
                ModelState.AddModelError("", Error.Description);
            }
        }
    }
}