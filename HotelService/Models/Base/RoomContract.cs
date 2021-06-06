using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class RoomContract
    {
        public RoomContract()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public User Client { get; set; }
        public Room Room { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
