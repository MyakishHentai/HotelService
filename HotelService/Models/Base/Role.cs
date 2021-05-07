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

    public enum Type
    {
        Admin,
        Client,
        Staff
    }
}
