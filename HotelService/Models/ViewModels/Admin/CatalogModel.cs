using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;

namespace HotelService.Models.ViewModels.Admin
{
    public class CatalogModel
    {
        public IEnumerable< Base.Service> Services { get; set; }
        public IEnumerable<ServiceCategory> Categories { get; set; }
    }
}
