using System.Linq;
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
    public class ServiceController : Controller
    {
        private HotelServiceContext m_Context;

        //public IUserValidator<User> UserValidator;
        //private IPasswordValidator<User> m_PasswordValidator;
        //private IPasswordHasher<User> m_PasswordHasher;
        private RoleManager<Role> m_RoleManager;
        private UserManager<User> m_UserManager;

        public ServiceController(UserManager<User> usrMgr, RoleManager<Role> roleMgr, HotelServiceContext context)
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
            var Services = m_Context.Services.Include(x => x.Category).AsNoTracking();
            return View(await Services.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Base.Service service)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SelectCategories = new SelectList(m_Context.ServiceCategories.ToList(), "CategoryId", "Title");
                return View(service);
            }
            m_Context.Services.Add(service);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Service = await m_Context.Services.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ServiceId == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
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
        public async Task<IActionResult> Edit(Models.Base.Service service)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SelectCategories =
                    new SelectList(
                        m_Context.ServiceCategories.Where(x => x.CategoryId != service.CategoryId).ToList(),
                        "CategoryId", "Title");
                return View(service);
            }

            m_Context.Services.Update(service);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null) return NotFound();
            var Services = await m_Context.Services.FirstOrDefaultAsync(p => p.ServiceId == id);
            if (Services != null)
                return View(Services);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var Services = await m_Context.Services.FirstOrDefaultAsync(p => p.ServiceId == id);
            if (Services == null) return NotFound();
            m_Context.Services.Remove(Services);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}