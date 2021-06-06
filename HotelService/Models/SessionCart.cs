using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HotelService.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.Models
{
    public class SessionCart : ShoppingCart
    {
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            var Session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var Cart = Session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            Cart.Session = Session;
            return Cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Base.Service service, int quantity)
        {
            base.AddItem(service, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Base.Service service)
        {
            base.RemoveLine(service);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
