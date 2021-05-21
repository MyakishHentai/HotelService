using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Request
    {
        public Request()
        {
            ServiceRequests = new HashSet<ServiceRequest>();
        }

        public int BasketId { get; set; }
        public int ServiceId { get; set; }
        public int ContractId { get; set; }
        public DateTime ConclusionDate { get; set; }
        public string Comment { get; set; }
        public int RepeatTally { get; set; }
        public decimal CostTotal { get; set; }

        public Basket Basket { get; set; }
        public RoomContract Contract { get; set; }
        public Service Service { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
