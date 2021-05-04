using System;
using System.Collections.Generic;

#nullable disable

namespace HotelService.Models.Base
{
    public partial class CategoriesService
    {
        public CategoriesService()
        {
            InverseSubCategory = new HashSet<CategoriesService>();
            Services = new HashSet<Service>();
        }

        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual CategoriesService SubCategory { get; set; }
        public virtual ICollection<CategoriesService> InverseSubCategory { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
