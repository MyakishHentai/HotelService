using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;

namespace HotelService.Repositories.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        IQueryable<Order> Orders { get; }
        IQueryable<RoomContract> Contracts{ get; }
        IQueryable<RoomContract> GetContractsForUser(string userId);
        void SaveOrder(Order order);
        Task Save();
    }
}
