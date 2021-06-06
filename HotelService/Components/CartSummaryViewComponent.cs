using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private ShoppingCart Cart;

        public CartSummaryViewComponent(ShoppingCart cartService)
        {
            Cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(Cart);
        }
    }
}
