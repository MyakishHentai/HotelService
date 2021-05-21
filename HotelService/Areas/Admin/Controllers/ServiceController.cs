using System.Linq;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            IQueryable<Models.Base.Service> Services = m_Context.Services.Include(x => x.Category);

            Services = Services.OrderBy(x => x.Title);

            return View(await Services.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Base.Service service)
        {
            m_Context.Services.Add(service);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Services = await m_Context.Services.FirstOrDefaultAsync(p => p.ServiceId == id);
            if (Services != null)
                return View(Services);
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var Services = await m_Context.Services.FirstOrDefaultAsync(p => p.ServiceId == id);
            if (Services != null)
                return View(Services);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Base.Service service)
        {
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