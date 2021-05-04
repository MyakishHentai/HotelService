using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class WorkStaff
    {
        public int WorkerId { get; set; }
        public int ServiceId { get; set; }
        public int BasketId { get; set; }
        public DateTime OrderTakeDate { get; set; }
        public DateTime? OrderDoneDate { get; set; }
        public string Comment { get; set; }

        public virtual Request Request { get; set; }
        public virtual User Worker { get; set; }
    }
}
