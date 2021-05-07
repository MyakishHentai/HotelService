using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Request
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

        public virtual Basket Basket { get; set; }
        public virtual RoomContract Contract { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
