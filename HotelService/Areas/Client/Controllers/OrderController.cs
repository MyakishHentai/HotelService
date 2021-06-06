using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Developer, Client")]
    public class OrderController : Controller
    {
        private ICatalogManager m_Catalog;
        private ShoppingCart m_Cart;

        public OrderController(ICatalogManager repoService, ShoppingCart cartService)
        {
            m_Catalog = repoService;
            m_Cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!m_Cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (!ModelState.IsValid) return View(order);

            order.Requests = m_Cart.Lines.ToArray();
            m_Catalog.SaveOrder(order);
            return RedirectToAction(nameof(Completed));

        }

        public ViewResult Completed()
        {
            m_Cart.Clear();
            return View();
        }
    }
}
