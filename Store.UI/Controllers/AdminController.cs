using ClassLibrary1;
using PagedList;
using Store.Domain;
using Store.DomainN;
using Store.Logic;
using Store.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Store.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IUnitOfWork _uow;
        public AdminController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: Admin
        public ActionResult ClothList(int? page)
        {
            var pageSize = 3;
            var clothes = _uow.Clothes.GetAll().ToList().ToPagedList(page ?? 1, pageSize);
            var cvm = new ClothListViewModel { Clothes = clothes };
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(cvm);
        }


        [HttpGet]
        public ActionResult CategoryList()
        {

            var categories = _uow.Categories.GetAll();//.Categories;
            var clvm = new CategoryListViewModel { Categories = categories };
            return View(clvm);
        }



        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditCloth(int id)
        {
            var _cloth = _uow.Clothes.Get(id);
            var categories = _uow.Categories.GetAll();//.Categories;

            var cat = _cloth.Category;


            ViewBag.Categories = new SelectList(categories, "Name", "Name", cat.Name);

            var cvm = new ClothViewModel
            {
                ShortDescription = _cloth.ShortDescription,
                LongDescription = _cloth.LongDescription,
                Price = _cloth.Price,
                Name = _cloth.Name,
                InStock = _cloth.InStock,
                IsFavorite = _cloth.IsFavorite,
                CategoryName = _cloth.Category.Name,
                Id = _cloth.Id,
                ImageUrl = _cloth.ImageUrl,
                //ImageFile = _cloth.
            };

            return View(cvm);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditCloth(ClothViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var cloth = _uow.Clothes.Get(cvm.Id);//.Find(c => c.Id == cvm.Id).FirstOrDefault();
                var cat = _uow.Categories.Find(r => r.Name == cvm.CategoryName).FirstOrDefault();//.Get(cvm.CategoryId);



                if (cloth != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(cvm.ImageFile.FileName);
                    string extension = Path.GetExtension(cvm.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    cvm.ImageUrl = fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);

                    cloth.Name = cvm.Name;
                    cloth.Price = cvm.Price;
                    cloth.ShortDescription = cvm.ShortDescription;
                    cloth.LongDescription = cvm.LongDescription;
                    cloth.ImageUrl = cvm.ImageUrl;
                    cloth.ImageThumbnailUrl = cvm.ImageUrl;
                    if (cvm.ImageFile != null && cvm.ImageFile.ContentLength > 0)
                    {
                        cvm.ImageFile.SaveAs(fileName);
                    }

                    cloth.InStock = cvm.InStock;
                    cloth.Category = cat;

                    _uow.Commit();

                    TempData["message"] = $"{cloth.Name} was successfully edited.";
                }



                return RedirectToAction("Index");
            }
            else
            {

                return View(cvm);
            }
        }

        public ActionResult DeleteCategory(int Id)
        {
            var _category = _uow.Categories.Find(c => c.Id == Id).FirstOrDefault();
            
            if (_category != null)
            {
                _uow.Categories.Remove(_category);
                _uow.Commit();

                TempData["message"] = $"{_category.Name} was successfully deleted.";

            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCloth(int Id)
        {
            var _cloth = _uow.Clothes.Find(c => c.Id == Id).FirstOrDefault();

            if (_cloth.ImageUrl != null)
            {
                List<string> files = Directory.EnumerateFiles("~/Content/Images").ToList();
                var fullPath = string.Empty;
                string fileName = _cloth.ImageUrl;
                string realFileName = files.Where(i => i.StartsWith(fileName)).FirstOrDefault();

                if (realFileName != null)
                {
                    fullPath = Path.Combine(Server.MapPath("~/Content/Images/"), realFileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        var ImgMessage = $"the image: {fullPath} was also removed.";
                    }

                }
            }






            if (_cloth != null)
            {
                _uow.Clothes.Remove(_cloth);
                _uow.Commit();

                TempData["message"] = $"{_cloth.Name} was successfully deleted.{Environment.NewLine}";

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new CategoryViewModel());
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var existingcat = _uow.Categories.Find(t => t.Name == cvm.Name.Trim()).FirstOrDefault();


                if (existingcat != null)
                {
                    ModelState.AddModelError("", "A category with that name already exists");
                    return View(cvm);
                }


                var cat = new Category
                {
                    Name = cvm.Name,
                    Clothes = new List<Cloth>(),
                    Description = cvm.Description,


                };


                _uow.Categories.Add(cat);
                _uow.Commit();

                TempData["message"] = string.Format("{0} has been saved.", cvm.Name);

                //ModelState.Clear();
                //cvm.Done = true;
                //return View(cvm);
    

                return RedirectToAction("Index");
            }
            else
            {
                //something went wrong with the form data values
                return View(cvm);
            }
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var category = _uow.Categories.Get(id);

            var cvm = new CategoryViewModel
            {
                Description = category.Description,

                Name = category.Name,

                Id = category.Id
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var category = _uow.Categories.Find(c => c.Id == cvm.Id).FirstOrDefault();

                if (category != null)
                {
                    category.Name = cvm.Name;
                    category.Description = cvm.Description;
                    _uow.Commit();
                    TempData["message"] = $"{category.Name} was successfully edited.";
                }



                return RedirectToAction("Index");
            }
            else
            {

                return View(cvm);
            }
        }


        [HttpGet]
        public ActionResult AddCloth()
        {

            //var categories = _uow.Categories.GetAll();//.Categories;
            var categories = _uow.Categories.GetAll().Select(r => r.Name);

            //ViewBag.Categories = new SelectList(categories, "Id", "Name");

            ViewBag.Categories = new SelectList(categories);

            return View(new ClothViewModel());
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddCloth(ClothViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var cat = _uow.Categories.Find(r => r.Name == cvm.CategoryName).FirstOrDefault();//_uow.Categories.Get(cvm.CategoryId);
                string fileName = Path.GetFileNameWithoutExtension(cvm.ImageFile.FileName);
                string extension = Path.GetExtension(cvm.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                cvm.ImageUrl = fileName;//"~/Content/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);

                //Save to the location on the directory
                cvm.ImageFile.SaveAs(fileName);

                var cloth = new Cloth
                {
                    Name = cvm.Name,
                    Price = cvm.Price,
                    ShortDescription = cvm.ShortDescription,
                    LongDescription = cvm.LongDescription,
                    ImageUrl = cvm.ImageUrl,
                    ImageThumbnailUrl = cvm.ImageUrl,
                    InStock = cvm.InStock,
                    Category = cat
                };


                _uow.Clothes.Add(cloth);
                _uow.Commit();

                TempData["message"] = string.Format("{0} has been saved.", cvm.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //something went wrong with the form data values
                var categories = _uow.Categories.GetAll().Select(r => r.Name);

                ViewBag.Categories = new SelectList(categories);
               // ModelState.Errors.

                return View(cvm);
            }
        }

        [HttpGet]
        public ActionResult UserList()
        {
           List<IClothAppUser> users = _uow.Users.UserList;

            List<User> userlist = users.Select(c => (User)c).ToList();

           
            var ulvm = new UserListViewModel { Users = userlist };
            return View(ulvm);

            
        }

      





    }
}