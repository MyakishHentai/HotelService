using System.Linq;
using HotelService.Infrastructure;
using HotelService.Models;
using HotelService.Models.ViewModels.Client;
using HotelService.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Developer, Client")]
    public class CartController : Controller
    {
        private ICatalogManager m_Catalog;
        private ShoppingCart m_Cart;

        public CartController(ICatalogManager catalog, ShoppingCart cart)
        {
            m_Catalog = catalog;
            m_Cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartModel
            {
                Cart = m_Cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var Product = m_Catalog.Services
                .FirstOrDefault(p => p.Id == id);

            if (Product != null)
            {
                m_Cart.AddItem(Product, 1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int id,
            string returnUrl)
        {
            var Product = m_Catalog.Services
                .FirstOrDefault(p => p.Id == id);

            if (Product != null)
            {
                m_Cart.RemoveLine(Product);
            }

            return RedirectToAction("Index", new {returnUrl});
        }
    }
}