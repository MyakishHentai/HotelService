using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            InverseSubCategory = new HashSet<ServiceCategory>();
            Services = new HashSet<Service>();
        }

        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Descriprion { get; set; }
        public string ImagePath { get; set; }

        public virtual ServiceCategory SubCategory { get; set; }
        public virtual ICollection<ServiceCategory> InverseSubCategory { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
