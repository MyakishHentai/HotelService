using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Feedback
    {
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public double Rating { get; set; }
        public string Review { get; set; }
        public DateTime IssueDate { get; set; }

        public virtual User Client { get; set; }
        public virtual Service Service { get; set; }
    }
}
