using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;

namespace HotelService.Repositories.Interfaces
{
    public interface ICatalogManager : IDisposable
    {
        IQueryable<ServiceCategory> Categories { get; }
        IQueryable<Models.Base.Service> Services { get; }

        public IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
        Task Save();
    }
}
