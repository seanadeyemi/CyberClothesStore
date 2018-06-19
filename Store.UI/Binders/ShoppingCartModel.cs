using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Services;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.UI.Binders
{
    public class ShoppingCartModelBinder : IModelBinder
    {
        private const string key = "Cart";


        //ClothShoppingCartService cart = null;

        //IShoppingCartService<Cloth> cartService;
        //public ShoppingCartModel(IShoppingCartService<Cloth> cartService)
        //{
        //    this.cartService = cartService;
        //}

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ShoppingCart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                //cart = (ClothShoppingCartService)controllerContext.HttpContext.Session[key];
                cart = (ShoppingCart)controllerContext.HttpContext.Session[key];
            }
            if (cart == null)
            {
                /*cart = (ShoppingCart)cartService*/
                cart = new ShoppingCart();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[key] = cart;
            }

            return cart;
        }
    }
}