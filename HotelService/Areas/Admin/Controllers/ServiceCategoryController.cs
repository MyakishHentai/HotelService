using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace HotelService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Developer")]
    public class ServiceCategoryController : Controller
    {
        private HotelServiceContext m_Context;
        private UserManager<User> m_UserManager;
        private readonly IWebHostEnvironment m_HostingEnvironment;
        public ServiceCategoryController(UserManager<User> usrMgr, HotelServiceContext context, IWebHostEnvironment hostingEnvironment)
        {
            m_UserManager = usrMgr;
            m_Context = context;
            m_HostingEnvironment = hostingEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            var Categories = m_Context.ServiceCategories.AsNoTracking().Include(x => x.SubCategory);
            return View(await Categories.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            // Создание новой категории
            if (id == null)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "Id", "Title");
                ViewBag.SelectSystems = new SelectList(await m_UserManager.GetUsersInRoleAsync(RoleType.SystemEmployee.ToString()), "Id", "UserName");
                return View(new ServiceCategory());
            }
            // Поиск заданной категории
            var Category = await m_Context.ServiceCategories.Include(x => x.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == id);

            // Отправка в представление тех опций, что еще не выбраны
            ViewBag.SelectCategories =
                new SelectList(
                    m_Context.ServiceCategories.AsNoTracking().Where(x => x.Id != Category.Id).ToList(), "Id", "Title");
            var Systems = await m_UserManager.GetUsersInRoleAsync(RoleType.SystemEmployee.ToString());
            ViewBag.SelectSystems = new SelectList(Systems.Where(x => x.Id != Category.SystemEmployeeId), "Id", "UserName");
            
            if (Category != null)
                return View(Category);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(ServiceCategory category, int? id, IFormFile imagePath)
        {
            if (ModelState.IsValid)
            {
                if (imagePath != null)
                {
                    category.ImagePath = imagePath.FileName;
                    await using var Stream = new FileStream(Path.Combine(m_HostingEnvironment.WebRootPath, "img/local/categories", imagePath.FileName), FileMode.Create);
                    await imagePath.CopyToAsync(Stream);
                }
                //Insert
                if (id == 0)
                {
                    m_Context.ServiceCategories.Add(category);
                    await m_Context.SaveChangesAsync();

                }
                //Update
                else
                {
                    m_Context.ServiceCategories.Update(category);
                    await m_Context.SaveChangesAsync();
                }
                //return RedirectToAction("Index");
                return Json(new { isValid = true, html = HelperView.RenderRazorViewToString(this, "_ViewTable", m_Context.ServiceCategories.ToList()) });
            }

      
            ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.AsNoTracking().ToList(), "Id", "Title");
            ViewBag.SelectSystems = new SelectList(await m_UserManager.GetUsersInRoleAsync(RoleType.SystemEmployee.ToString()), "Id", "UserName");
            
            // TODO: Добавить сообщение об ошибках - повторении Index
            return Json(new { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEdit", category) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Category = await m_Context.ServiceCategories.AsNoTracking()
                .Include(x => x.SubCategory)
                .Include(x => x.SystemEmployee)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Category != null)
                return View(Category);
            return NotFound();
        }


        [HttpGet]
        [NoDirectAccess]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.AsNoTracking().Include(s => s.SubCategory).FirstOrDefaultAsync(p => p.Id == id);
            if (Categories != null)
                return View(Categories);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.FirstOrDefaultAsync(p => p.Id == id);
            if (Categories == null) return NotFound();
            m_Context.ServiceCategories.Remove(Categories);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}