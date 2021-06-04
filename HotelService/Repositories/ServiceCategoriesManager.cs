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
    public class ServiceCategoriesManager : IRepository<ServiceCategory, int>
    {
        HotelServiceContext m_Context;
        public ServiceCategoriesManager(HotelServiceContext context)
        {
            m_Context = context;
        }

        private bool m_Disposed = false;

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

        public IQueryable<ServiceCategory> GetAll()
        {
            throw new NotImplementedException();
        }

        public ServiceCategory Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(ServiceCategory item)
        {
            throw new NotImplementedException();
        }

        public void Update(ServiceCategory item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await m_Context.SaveChangesAsync();
        }
    }
}
