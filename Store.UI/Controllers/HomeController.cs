using Store.Domain;
using Store.Logic.Common;
using Store.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.UI.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork _uow;
        public HomeController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult Index()
        {
            var latestClothes = _uow.Clothes.GetCheapestClothes;
            var hvm = new HomeViewModel { FeaturedClothes = latestClothes };

            return View(hvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}