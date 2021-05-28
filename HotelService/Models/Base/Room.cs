using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Room
    {
        public Room()
        {
            RoomContracts = new HashSet<RoomContract>();
        }

        public int Id { get; set; }
        public int BuildingId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Cost { get; set; }
        public string ImagePath { get; set; }

        public Building Building { get; set; }
        public ICollection<RoomContract> RoomContracts { get; set; }
    }
}
