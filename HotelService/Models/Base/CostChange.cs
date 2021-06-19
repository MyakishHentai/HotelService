using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class CostChange
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime ChangeDate { get; set; }
        public decimal NewCostValue { get; set; }

        public Service Service { get; set; }
    }
}
