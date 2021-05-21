using System;

#nullable disable

namespace HotelService.Models.Base
{
    public class Feedback
    {
        public string ClientId { get; set; }
        public int ServiceId { get; set; }
        public double Rating { get; set; }
        public string Review { get; set; }
        public DateTime WritingDate { get; set; }

        public User Client { get; set; }
        public Service Service { get; set; }
    }
}
