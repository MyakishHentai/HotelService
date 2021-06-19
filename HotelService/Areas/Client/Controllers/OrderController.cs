using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public OrderController(ICatalogManager repoService, ShoppingCart cartService, IOrderManager orderManagerManager, SignInManager<User> signIn)
        {
            m_CatalogManager = repoService;
            m_Cart = cartService;
            m_OrderManager = orderManagerManager;
            m_SignInManager = signIn;
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
    }
}
