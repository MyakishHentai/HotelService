using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Room
    {
        public Room()
        {
            RoomContracts = new HashSet<RoomContract>();
        }

        public int RoomId { get; set; }
        public int BuildingId { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Cost { get; set; }
        public int? ImageId { get; set; }

        public virtual Building Building { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<RoomContract> RoomContracts { get; set; }
    }
}
