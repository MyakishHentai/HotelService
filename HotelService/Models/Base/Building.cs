using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Building
    {
        public Building()
        {
            Rooms = new HashSet<Room>();
        }

        public int BuildingId { get; set; }
        public string AdministratorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Descriprion { get; set; }
        public string ImagePath { get; set; }

        public virtual User Administrator { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
