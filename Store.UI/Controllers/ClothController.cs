using Store.Domain;
using Store.Logic;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.UI.Controllers
{
    public class ClothController : Controller
    {
        IUnitOfWork _uow;
        public ClothController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Details(int id)
        {
            var _cloth = _uow.Clothes.Get(id);
            Cloth cloth = (Cloth)_cloth;       

            return View(cloth);
        }
        public ActionResult QuickView(int id)
        {
           var _cloth = _uow.Clothes.Get(id);

           Cloth cloth = (Cloth)_cloth;
            
            return PartialView("QuickView", cloth);
        }


        

     
    }
}