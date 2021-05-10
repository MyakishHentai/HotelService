using HotelService.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Clent, Guest")]
    public class HomeController : Controller
    {
        private UserManager<User> m_UserManager;
        //public IUserValidator<User> UserValidator;
        //private IPasswordValidator<User> m_PasswordValidator;
        //private IPasswordHasher<User> m_PasswordHasher;
        private RoleManager<Role> m_RoleManager;

        public HomeController(UserManager<User> usrMgr, RoleManager<Role> roleMgr)
        {
            m_UserManager = usrMgr;
            m_RoleManager = roleMgr;
        }

        public ViewResult Index() => View();

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var Error in result.Errors)
            {
                ModelState.AddModelError("", Error.Description);
            }
        }
    }
}
