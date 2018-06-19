using Store.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.UI.Controllers
{
    public class NavigationController : Controller
    {
        private readonly IUnitOfWork _uow;

        public NavigationController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<ICategory> categories = _uow.Categories.GetAll().Distinct().OrderBy(x => x);



            return PartialView(categories);
        }
    }
}