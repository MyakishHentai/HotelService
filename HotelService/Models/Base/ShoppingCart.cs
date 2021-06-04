using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            RequestStates = new HashSet<RequestState>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal CostTotal { get; set; }

        public ICollection<RequestState> RequestStates { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
