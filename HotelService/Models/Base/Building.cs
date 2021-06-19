using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class Building
    {
        public Building()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string AdminId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public User Admin { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
