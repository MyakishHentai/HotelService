using Microsoft.AspNetCore.Identity;

namespace HotelService.Models.Base
{
    public class Role : IdentityRole
    {
        public Role()
        { }

        public Role(string name)
            : base(name)
        { }
    }

    public enum RoleType
    {
        Visitor,
        Client,
        Admin,
        SystemEmployee
    }
}
