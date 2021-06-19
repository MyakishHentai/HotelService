using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Feedback
    {
        public string ClientId { get; set; }
        public int ServiceId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime WritingDate { get; set; }

        public User Client { get; set; }
        public Service Service { get; set; }
    }
}
