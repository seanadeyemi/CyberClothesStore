using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Services;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.UI.Models
{
    public class CartIndexViewModel
    {
        public ShoppingCart Cart { get; set; }

        public string ReturnUrl { get; set; }

    }
}