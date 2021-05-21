﻿using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Developer")]
    public class ServiceCategoryController : Controller
    {
        private HotelServiceContext m_Context;

        //public IUserValidator<User> UserValidator;
        //private IPasswordValidator<User> m_PasswordValidator;
        //private IPasswordHasher<User> m_PasswordHasher;
        private RoleManager<Role> m_RoleManager;
        private UserManager<User> m_UserManager;

        public ServiceCategoryController(UserManager<User> usrMgr, RoleManager<Role> roleMgr,
            HotelServiceContext context)
        {
            m_UserManager = usrMgr;
            m_RoleManager = roleMgr;
            m_Context = context;
        }

        /// <summary>
        ///     Errors.
        /// </summary>
        /// <param name="result"></param>
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var Error in result.Errors) ModelState.AddModelError("", Error.Description);
        }

        public async Task<IActionResult> Index()
        {
            var Categories= m_Context.ServiceCategories.Include(x => x.SubCategory).AsNoTracking();
            return View(await Categories.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCategory category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");
                return View(category);
            }
            m_Context.ServiceCategories.Add(category);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Category = await m_Context.ServiceCategories.Include(x => x.SubCategory)
                .FirstOrDefaultAsync(x => x.CategoryId == id);
            if (Category != null)
                return View(Category);
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.Include(x => x.SubCategory)
                .FirstOrDefaultAsync(p => p.CategoryId == id);
            ViewBag.SelectCategories =
                new SelectList(
                    m_Context.ServiceCategories.Where(x => x.CategoryId != Categories.SubCategoryId).ToList(),
                    "CategoryId", "Title");
            if (Categories != null)
                return View(Categories);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceCategory category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SelectCategories =
                    new SelectList(
                        m_Context.ServiceCategories.Where(x => x.CategoryId != category.SubCategoryId).ToList(),
                        "CategoryId", "Title");
                return View(category);
            }

            m_Context.ServiceCategories.Update(category);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.FirstOrDefaultAsync(p => p.CategoryId == id);
            if (Categories != null)
                return View(Categories);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.FirstOrDefaultAsync(p => p.CategoryId == id);
            if (Categories == null) return NotFound();
            m_Context.ServiceCategories.Remove(Categories);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}