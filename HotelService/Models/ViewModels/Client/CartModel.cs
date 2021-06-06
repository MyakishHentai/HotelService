using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Models.ViewModels.Client
{
    public class CartModel
    {
        public ShoppingCart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
