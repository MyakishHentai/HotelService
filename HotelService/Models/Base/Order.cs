using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Order
    {
        public Order()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int RoomContractId { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string PaymentDetails { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal CostTotal { get; set; }

        public RoomContract RoomContract { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
