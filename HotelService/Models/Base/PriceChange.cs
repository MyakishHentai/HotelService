using System;

#nullable disable

namespace HotelService.Models.Base
{
    public class PriceChange
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime ChangeDate { get; set; }
        public decimal NewPriceValue { get; set; }

        public Service Service { get; set; }
    }
}
