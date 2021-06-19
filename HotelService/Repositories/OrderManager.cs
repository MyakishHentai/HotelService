using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Repositories
{
    public class OrderManager : IOrderManager
    {
        private bool m_Disposed = false;
        HotelServiceContext m_Context;
        private RoleManager<Role> m_RoleManager;
        private UserManager<User> m_UserManager;
        public OrderManager(HotelServiceContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            m_Context = context;
            m_RoleManager = roleManager;
            m_UserManager = userManager;
        }

        public IQueryable<ServiceCategory> Categories => m_Context.ServiceCategories.AsQueryable();
        public IQueryable<Models.Base.Service> Services => m_Context.Services.AsQueryable();

        public IQueryable<Order> Orders => m_Context.Orders.Include(i => i.Requests).ThenInclude(l => l.Service);
        public IQueryable<RoomContract> Contracts => m_Context.RoomContracts.AsQueryable();

        public IQueryable<RoomContract> GetContractsForUser(string userId)
        {
            var RoomContracts = Contracts.AsNoTracking().Where(x => x.Client.Id == userId).Include(x => x.Room);
            return RoomContracts;
        }
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
