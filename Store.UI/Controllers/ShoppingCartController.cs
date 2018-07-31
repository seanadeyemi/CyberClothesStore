using Store.Domain;
using Store.DomainN;
using Store.Infrastructure.Services;
using Store.Logic;
using Store.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace Store.UI.Controllers
{
    [AllowAnonymous]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _uow;
        public ShoppingCartController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult Index(ShoppingCart cartService, string returnUrl)
        {
            var civm = new CartIndexViewModel
            {
                Cart = cartService,
                ReturnUrl = returnUrl
            };
            return View(civm);
        }




        public PartialViewResult Summary(ShoppingCart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }


        public ActionResult PrintPdf(ShoppingCart cart)
        {
            //var pdfView = new ActionAsPdf("Index", new { cartService = cart,  returnUrl = ""});
            var civm = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = ""
            };


            //var pdfView = new ViewAsPdf("Index", civm);

            var pdfView = new ViewAsPdf("Index", civm)
            {
                FileName = "File.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Left = 0, Right = 0 }
            };


            return pdfView;
        }


        //public PartialViewResult LineSubTotal(ShoppingCart cart, int lineId, int Quantity)
        //{
        //    OrderDetail line = cart.Items.FirstOrDefault(c => c.Id == lineId);
        //    if(line != null)
        //    {
        //        line.Quantity = Quantity;
        //    }
        //    Cloth cloth = (Cloth)line.Cloth;
        //    cart.RemoveItem(cloth);

        //    cart.AddItem(cloth, Quantity);

        //    return PartialView(line);
        //}


        [Authorize]
        [HttpPost]
        public ViewResult Checkout(ShoppingCart cart, ShippingDetails shippingDetails)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }

        }

        public ActionResult AddToCart(ShoppingCart cart, int ClothId, string returnUrl, bool IsPartial = true)
        {
            ICloth cloth = _uow.Clothes.Get(ClothId);
            if (cloth != null)
            {

                cart.AddItem((Cloth)cloth, 1);
            }
            if (IsPartial)
            {
                return RedirectToAction("Summary");
            }
            else
            {
                return RedirectToAction("Index", new { returnUrl });
            }

        }



        public ActionResult RemoveFromCart(ShoppingCart cart, int ClothId, string returnUrl, bool IsPartial = false)
        {
            ICloth cloth = _uow.Clothes.Get(ClothId);
            if (cloth != null)
            {

                cart.RemoveItem((Cloth)cloth);
            }
            if (IsPartial)
            {
                return RedirectToAction("Index", "Home");//new EmptyResult();
            }


            return RedirectToAction("Index", new { returnUrl });
        }

        //private ClothShoppingCartService GetCart()
        //{
        //    ClothShoppingCartService cart = (ClothShoppingCartService)Session["Cart"];
        //    ShoppingCart sc = new ShoppingCart();

        //    if (cart == null)
        //    {
        //        cart = new ClothShoppingCartService(sc);
        //        Session["Cart"] = cart;
        //    }

        //    return cart;
        //}


    }
}