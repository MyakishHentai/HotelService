using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Request
    {
        public Request()
        {
            RequestStates = new HashSet<RequestState>();
        }

        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int RoomContractId { get; set; }
        public int ServiceId { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }

        public RoomContract RoomContract { get; set; }
        public Service Service { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public ICollection<RequestState> RequestStates { get; set; }
    }
}
