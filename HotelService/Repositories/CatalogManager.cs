using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Repositories
{
    public class CatalogManager : ICatalogManager
    {
        private bool m_Disposed = false;
        HotelServiceContext m_Context;
        public CatalogManager(HotelServiceContext context)
        {
            m_Context = context;
        }

        public IQueryable<ServiceCategory> Categories => m_Context.ServiceCategories.AsQueryable();
        public IQueryable<Models.Base.Service> Services => m_Context.Services.AsQueryable();

        public IQueryable<Order> Orders => m_Context.Orders.Include(o => o.Requests).ThenInclude(l => l.Service);

        public void SaveOrder(Order order)
        {
            m_Context.AttachRange(order.Requests.Select(l => l.Service));
            if (order.Id == 0)
            {
                m_Context.Orders.Add(order);
            }

            m_Context.SaveChanges();
        }

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
