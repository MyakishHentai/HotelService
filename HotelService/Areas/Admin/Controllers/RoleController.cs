using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Models.ViewModels.Admin;
using HotelService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HotelService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Developer")]
    public class RoleController : Controller
    {
        private readonly HotelServiceContext m_Context;
        private RoleManager<Role> m_RoleManager;
        private UserManager<User> m_UserManager;

        public RoleController(UserManager<User> usrMgr, RoleManager<Role> roleMgr, HotelServiceContext context)
        {
            m_UserManager = usrMgr;
            m_RoleManager = roleMgr;
            m_Context = context;
        }

        public ViewResult Index() => View(m_RoleManager.Roles);



        [HttpGet]
        [NoDirectAccess]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (id == null) return NotFound();
            var Role = await m_RoleManager.FindByIdAsync(id);
            if (Role != null)
                return View(Role);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Role role = await m_RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await m_RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", m_RoleManager.Roles);
        }

        public async Task<IActionResult> Edit(string id)
        {

            Role role = await m_RoleManager.FindByIdAsync(id);
            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();
            foreach (User user in m_UserManager.Users)
            {
                var list = await m_UserManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    User user = await m_UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await m_UserManager.AddToRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    User user = await m_UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await m_UserManager.RemoveFromRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
