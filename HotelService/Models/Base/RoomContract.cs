using System;

#nullable disable

namespace HotelService.Models.Base
{
    public class RoomContract
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public User Client { get; set; }
        public Room Room { get; set; }
    }
}
