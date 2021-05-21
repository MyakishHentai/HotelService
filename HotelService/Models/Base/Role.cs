using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models.Base
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string name)
            : base(name)
        { }
    }

    //public static class RoleTypes
    //{
    //    public static string[] Types => new[] {$"{RoleType.Visitor}", $"{RoleType.Client}", $"{RoleType.Admin}", $"{RoleType.Employee}" };
    //}

    public enum RoleType
    {
        Visitor,
        Client,
        Admin,
        Employee
    }
}
