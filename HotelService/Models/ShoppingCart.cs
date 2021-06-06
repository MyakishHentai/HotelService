using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Models.Base;

namespace HotelService.Models
{
    public class ShoppingCart
    {
        public List<Request> Lines { get; set; } = new List<Request>();
        public virtual void AddItem(Base.Service service, int quantity)
        {
            var Line = Lines.FirstOrDefault(p => p.Service.Id == service.Id);
            if (Line == null)
            {
                Lines.Add(new Request
                {
                    Service = service,
                    Quantity = quantity
                });
            }
            else
            {
                Line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Base.Service product) => Lines.RemoveAll(l => l.Service.Id == product.Id);
        public virtual decimal ComputeTotalValue() => Lines.Sum(e => e.Service.Cost * e.Quantity);
        public virtual void Clear() => Lines.Clear();
    }
}
