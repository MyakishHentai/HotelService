using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

#nullable disable

namespace HotelService.Models.Base
{
    [BindProperties]
    public class ServiceCategory
    {
        public ServiceCategory()
        {
            InverseSubCategory = new HashSet<ServiceCategory>();
            Services = new HashSet<Service>();
        }

        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

        
        [Required, RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }


        [StringLength(100, MinimumLength = 3)]
        public string Subtitle { get; set; }


        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ServiceCategory SubCategory { get; set; }
        public ICollection<ServiceCategory> InverseSubCategory { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
