using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;

namespace HotelService.Repositories.Interfaces
{
    public interface IShopManager : IDisposable
    {

        Task Save();
    }
}
