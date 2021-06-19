using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Favorite
    {
        public string ClientId { get; set; }
        public int ServiceId { get; set; }
        public bool? ShowState { get; set; }

        public User Client { get; set; }
        public Service Service { get; set; }
    }
}
