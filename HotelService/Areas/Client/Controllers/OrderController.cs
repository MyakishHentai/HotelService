using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories.Interfaces;
using HotelService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Developer, Client")]
    public class OrderController : Controller
    {
        private ICatalogManager m_CatalogManager;
        private IOrderManager m_OrderManager;
        private ShoppingCart m_Cart;
        private SignInManager<User> m_SignInManager;
        private HotelServiceContext m_Context;
        public OrderController(ICatalogManager repoService, ShoppingCart cartService, IOrderManager orderManagerManager, SignInManager<User> signIn, HotelServiceContext context)
        {
            m_CatalogManager = repoService;
            m_Cart = cartService;
            m_OrderManager = orderManagerManager;
            m_SignInManager = signIn;
            m_Context = context;
        }

        public async Task<ViewResult> Checkout()
        {
            var User = await m_SignInManager.UserManager.FindByNameAsync(base.User.Identity.Name);
            var Contracts = m_OrderManager.GetContractsForUser(User.Id).ToList();
            ViewBag.SelectContracts = new SelectList(Contracts, "Id", "Room.Number", "Room.Number", "ConclusionDate");
            return View(new Order{CostTotal = m_Cart.ComputeTotalValue()});
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!m_Cart.Lines.Any())
            {
                ModelState.AddModelError("", "Простите, вы не добавили ни одной услуги!");
            }

            if (!ModelState.IsValid) return View(order);

            order.Requests = m_Cart.Lines.ToArray();
            m_OrderManager.SaveOrder(order);
            m_Cart.Clear();
            return RedirectToAction(nameof(Completed));

        }

        public ViewResult Completed()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var Services = m_Context.Services.AsNoTracking().Include(x => x.ServiceCategory).Where(x => x.ServiceCategoryId == 1);
            return View(await Services.ToListAsync());
        }

        
        [NoDirectAccess]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var Service = await m_Context.Services.AsNoTracking()
                .Include(x => x.ServiceCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }


        [HttpGet]
        [NoDirectAccess]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null) return NotFound();
            var Service = await m_Context.Services.Include(s => s.ServiceCategory).FirstOrDefaultAsync(p => p.Id == id);
            if (Service != null)
                return View(Service);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var Service = await m_Context.Services.FirstOrDefaultAsync(p => p.Id == id);
            if (Service == null) return NotFound();
            m_Context.Services.Remove(Service);
            await m_Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
