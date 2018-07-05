using Store.Domain;
using Store.Logic;
using Store.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.UI.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork _uow;
        public AdminController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: Admin
        public ActionResult Index()
        {

            var clothes = _uow.Clothes.GetAll();
            var cvm = new ClothViewModel {  Clothes = clothes};

            return View(cvm);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var _cloth = _uow.Clothes.Get(id);

            return View(_cloth);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Cloth cloth)
        {
            if (ModelState.IsValid)
            {
                var _cloth = _uow.Clothes.Find(c => c.Id == cloth.Id).FirstOrDefault();
                if (_cloth != null)
                {
                    _uow.Clothes.Remove(_cloth);
                    _uow.Clothes.Add(cloth);
                    TempData["message"] = $"{_cloth.Name} was successfully edited.";
                }



                    return RedirectToAction("Index");
            }
            else
            {

                return View(cloth);
            }
        }

        public ActionResult Delete(int Id)
        {
            var _cloth = _uow.Clothes.Find(c => c.Id == Id).FirstOrDefault();

            if (_cloth != null)
            {
                _uow.Clothes.Remove(_cloth);

                TempData["message"] = $"{_cloth.Name} was successfully deleted.";

            }
            return RedirectToAction("Index");
        }







    }
}