using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public class ServiceCategory
    {
        public ServiceCategory()
        {
            InverseSubCategory = new HashSet<ServiceCategory>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool? AvailableState { get; set; }

        public ServiceCategory SubCategory { get; set; }
        public ICollection<ServiceCategory> InverseSubCategory { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
