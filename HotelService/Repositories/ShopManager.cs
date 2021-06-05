using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories.Interfaces;

namespace HotelService.Repositories
{
    public class ShopManager : IShopManager
    {
        private bool m_Disposed = false;
        HotelServiceContext m_Context;
        public ShopManager(HotelServiceContext context)
        {
            m_Context = context;
        }

        public IQueryable<ServiceCategory> Categories => m_Context.ServiceCategories.AsQueryable();
        public IQueryable<Models.Base.Service> Services => m_Context.Services.AsQueryable();

        public async Task Save()
        {
            await m_Context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.m_Disposed)
            {
                if (disposing)
                {
                    m_Context.Dispose();
                }
            }
            this.m_Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
