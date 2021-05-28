using System.Linq;
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
    public class ServiceController : Controller
    {
        private HotelServiceContext m_Context;

        public ServiceController(HotelServiceContext context)
        {
            m_Context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Services = m_Context.Services.Include(x => x.Category).AsNoTracking();
            return View(await Services.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");
                return View(new Models.Base.Service());
            }

            var Service = await m_Context.Services.Include(x => x.Category)
                .FirstOrDefaultAsync(p => p.ServiceId == id);
            ViewBag.SelectCategories =
                new SelectList(
                    m_Context.ServiceCategories.Where(x => x.CategoryId != Service.CategoryId).ToList(),
                    "CategoryId", "Title");
            if (Service != null)
                return View(Service);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(Models.Base.Service service, int? id)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    m_Context.Services.Add(service);
                    await m_Context.SaveChangesAsync();
                }
                //Update
                else
                {
                    m_Context.Services.Update(service);
                    //m_Context.Entry(service).State = EntityState.Modified;
                    await m_Context.SaveChangesAsync();
                }
                //return RedirectToAction("Index");
                return Json(new { isValid = true, html = HelperView.RenderRazorViewToString(this, "_ViewTable", m_Context.Services.Include(c => c.Category).ToList()) });
            }

            if (id == null)
            {
                ViewBag.SelectCategories = new SelectList(await m_Context.ServiceCategories.ToListAsync(), "CategoryId", "Title");
            }
            else
            {
                ViewBag.SelectCategories =
                    new SelectList(
                        await m_Context.ServiceCategories.Where(x => x.CategoryId != service.CategoryId).ToListAsync(),
                        "CategoryId", "Title");
            }
            // TODO: Добавить сообщение об ошибках - повторении Index
            return Json(new { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEdit", service) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Service = await m_Context.Services.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ServiceId == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }


        [HttpGet]
        [NoDirectAccess]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null) return NotFound();
            var Service = await m_Context.Services.Include(s => s.Category).FirstOrDefaultAsync(p => p.ServiceId == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var Service = await m_Context.Services.FirstOrDefaultAsync(p => p.ServiceId == id);
            if (Service == null) return NotFound();
            m_Context.Services.Remove(Service);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}