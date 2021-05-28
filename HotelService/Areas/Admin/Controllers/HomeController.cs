using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class HomeController : Controller
    {
        private UserManager<User> m_UserManager;
        //public IUserValidator<User> UserValidator;
        //private IPasswordValidator<User> m_PasswordValidator;
        //private IPasswordHasher<User> m_PasswordHasher;
        private RoleManager<Role> m_RoleManager;
        private HotelServiceContext m_Context;

        public HomeController(UserManager<User> usrMgr, RoleManager<Role> roleMgr, HotelServiceContext context)
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
            foreach (var Error in result.Errors)
            {
                ModelState.AddModelError("", Error.Description);
            }
        }

        public ViewResult Index()
        {
            return View();
        }
    }
}
