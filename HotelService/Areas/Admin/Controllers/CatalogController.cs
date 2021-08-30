using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Models.ViewModels.Admin;
using HotelService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Developer")]
    public class CatalogController : Controller
    {
        private const string PathImg = "img/local/services";
        private HotelServiceContext m_Context;
        private readonly IWebHostEnvironment m_HostingEnvironment;
        private UserManager<User> m_UserManager;
        public CatalogController(UserManager<User> usrMgr, HotelServiceContext context, IWebHostEnvironment hostingEnvironment)
        {
            m_Context = context;
            m_UserManager = usrMgr;
            m_HostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var Services = m_Context.Services.AsNoTracking().Include(x => x.ServiceCategory);
            var Categories = m_Context.ServiceCategories.AsNoTracking().Include(x => x.SubCategory);
            var Catalog = new CatalogModel { Services = await Services.ToListAsync(), Categories = await Categories.ToListAsync() };
            //return View(await Categories.ToListAsync());
            return View(Catalog);
        }


        [NoDirectAccess]
        public async Task<IActionResult> CreateEditCategory(int? id)
        {
            // Создание новой категории
            if (id is null or 0)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "Id", "Title");                
                return View(new ServiceCategory());
            }
            // Поиск заданной категории
            var Category = await m_Context.ServiceCategories.Include(x => x.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == id);

            // Отправка в представление тех опций, что еще не выбраны
            ViewBag.SelectCategories =
                new SelectList(
                    m_Context.ServiceCategories.AsNoTracking().Where(x => x.Id != Category.Id).ToList(), "Id", "Title");

            if (Category != null)
                return View(Category);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEditCategory(ServiceCategory category, int? id, IFormFile imagePath)
        {
            if (ModelState.IsValid)
            {
                if (imagePath != null)
                {
                    category.ImagePath = Path.Combine(PathImg, imagePath.FileName);
                    await using var Stream = new FileStream(Path.Combine(m_HostingEnvironment.WebRootPath, PathImg, imagePath.FileName), FileMode.Create);
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
                return Json(new { isValid = true, html = HelperView.RenderRazorViewToString(this, "_ViewCategories", m_Context.ServiceCategories.ToList()) });
            }


            ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.AsNoTracking().ToList(), "Id", "Title");
           
            // TODO: Добавить сообщение об ошибках - повторении Index
            return Json(new { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEditCategory", category) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> DetailsCategory(int? id)
        {
            if (id == null) return NotFound();

            var Category = await m_Context.ServiceCategories.AsNoTracking()
                .Include(x => x.SubCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Category != null)
                return View(Category);
            return NotFound();
        }


        [HttpGet]
        [NoDirectAccess]
        [ActionName("DeleteCategory")]
        public async Task<IActionResult> ConfirmDeleteCategory(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.AsNoTracking().Include(s => s.SubCategory).FirstOrDefaultAsync(p => p.Id == id);
            if (Categories != null)
                return View(Categories);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return NotFound();
            var Categories = await m_Context.ServiceCategories.FirstOrDefaultAsync(p => p.Id == id);
            if (Categories == null) return NotFound();
            m_Context.ServiceCategories.Remove(Categories);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateEditService(int? id)
        {
            if (id is null or 0)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "Id", "Title");
                return View(new Models.Base.Service());
            }

            var Service = await m_Context.Services.Include(x => x.ServiceCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
            ViewBag.SelectCategories =
                new SelectList(
                    m_Context.ServiceCategories.Where(x => x.Id != Service.ServiceCategoryId).ToList(),
                    "Id", "Title");
            if (Service != null)
                return View(Service);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEditService(Models.Base.Service service, int? id, IFormFile imagePath)
        {
            if (ModelState.IsValid)
            {
                if (imagePath != null)
                {
                    service.ImagePath = Path.Combine(PathImg, imagePath.FileName);
                    await using var Stream = new FileStream(Path.Combine(m_HostingEnvironment.WebRootPath, PathImg, imagePath.FileName), FileMode.Create);
                    await imagePath.CopyToAsync(Stream);
                }
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
                return Json(new { isValid = true, html = HelperView.RenderRazorViewToString(this, "_ViewServices", m_Context.Services.Include(c => c.ServiceCategory).ToList()) });
            }

            if (id == null)
            {
                ViewBag.SelectCategories = new SelectList(await m_Context.ServiceCategories.ToListAsync(), "Id", "Title");
            }
            else
            {
                ViewBag.SelectCategories =
                    new SelectList(
                        await m_Context.ServiceCategories.Where(x => x.Id != service.ServiceCategoryId).ToListAsync(),
                        "Id", "Title");
            }
            // TODO: Добавить сообщение об ошибках - повторении Index
            return Json(new { isValid = false, html = HelperView.RenderRazorViewToString(this, "CreateEditService", service) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> DetailsService(int? id)
        {
            if (id == null) return NotFound();

            var Service = await m_Context.Services.AsNoTracking()
                .Include(x => x.ServiceCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }


        [HttpGet]
        [NoDirectAccess]
        [ActionName("DeleteService")]
        public async Task<IActionResult> ConfirmDeleteService(int? id)
        {
            if (id == null) return NotFound();
            var Service = await m_Context.Services.Include(s => s.ServiceCategory).FirstOrDefaultAsync(p => p.Id == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(int? id)
        {
            if (id == null) return NotFound();
            var Service = await m_Context.Services.FirstOrDefaultAsync(p => p.Id == id);
            if (Service == null) return NotFound();
            m_Context.Services.Remove(Service);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}