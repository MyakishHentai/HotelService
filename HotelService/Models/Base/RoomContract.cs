using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class RoomContract
    {
        public RoomContract()
        {
            Requests = new HashSet<Request>();
        }

        public int ContractId { get; set; }
        public string ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public virtual User Client { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
