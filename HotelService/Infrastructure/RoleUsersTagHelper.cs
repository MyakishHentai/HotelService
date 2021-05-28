using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HotelService.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<User> m_UserManager;
        private RoleManager<Role> m_RoleManager;

        public RoleUsersTagHelper(UserManager<User> usermgr,
            RoleManager<Role> rolemgr)
        {
            m_UserManager = usermgr;
            m_RoleManager = rolemgr;
        }

        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            int Users = 0;
            //var Names = new List<string>();
            var FindRole = await m_RoleManager.FindByIdAsync(Role);
            if (FindRole != null)
            {
                foreach (var User in m_UserManager.Users)
                {
                    if (User != null
                        && await m_UserManager.IsInRoleAsync(User, FindRole.Name))
                    {
                        Users++;
                        //Names.Add(User.UserName);
                    }
                }
            }


            output.Content.SetContent(Users != 0 ? Users.ToString() : "No Users");
        }
    }
}
