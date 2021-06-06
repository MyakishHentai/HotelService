using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;

namespace HotelService.Models.ViewModels.Client
{
    public class CatalogModel
    {
        public IQueryable<Base.Service> Services { get; set; }
        public IQueryable<ServiceCategory> Categories { get; set; }
    }
}
