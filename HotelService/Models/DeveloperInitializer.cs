using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;
using HotelService.Service;
using Microsoft.AspNetCore.Identity;

namespace HotelService.Models
{
    public class DeveloperInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await userManager.FindByNameAsync(Developer.Name) == null)
            {
                if (await roleManager.FindByNameAsync(Developer.Role) == null)
                {
                    var Role = new Role(Developer.Role);
                    var ResultRole = await roleManager.CreateAsync(Role);
                }

                var User = new User
                {
                    UserName = Developer.Email,
                    FirstName = Developer.FirstName,
                    LastName = Developer.LastName,
                    Passport = Developer.Passport,
                    Email = Developer.Email,
                };

                var Result = await userManager.CreateAsync(User, Developer.Password);
                if (Result.Succeeded) await userManager.AddToRoleAsync(User, Developer.Role);
            }
        }
    }
}
