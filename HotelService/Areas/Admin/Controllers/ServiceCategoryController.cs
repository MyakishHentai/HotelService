using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Service;
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

        public ServiceCategoryController(HotelServiceContext context)
        {
            m_Context = context;
        }


        public async Task<IActionResult> Index()
        {
            var Categories = m_Context.ServiceCategories.Include(x => x.SubCategory).AsNoTracking();
            return View(await Categories.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");
                return View(new ServiceCategory());
            }
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(ServiceCategory category, int? id)
        {
            if (ModelState.IsValid)
            {
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

      
            ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");

            // TODO: Добавить сообщение об ошибках - повторении Index
            return Json(new { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEdit", category) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Category = await m_Context.ServiceCategories.Include(x => x.SubCategory)
                .FirstOrDefaultAsync(x => x.CategoryId == id);
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
            var Categories = await m_Context.ServiceCategories.Include(s => s.SubCategory).FirstOrDefaultAsync(p => p.CategoryId == id);
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