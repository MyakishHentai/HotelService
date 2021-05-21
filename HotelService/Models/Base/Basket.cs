using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Basket
    {
        public Basket()
        {
            Requests = new HashSet<Request>();
        }

        public int BasketId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal CostTotal { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
