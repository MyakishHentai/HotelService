using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class Image
    {
        public Image()
        {
            Buildings = new HashSet<Building>();
            CategoriesServices = new HashSet<CategoriesService>();
            Rooms = new HashSet<Room>();
            Services = new HashSet<Service>();
            Users = new HashSet<User>();
        }

        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageBin { get; set; }
        public string ImageFormat { get; set; }
        public string ImageDescription { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<CategoriesService> CategoriesServices { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
