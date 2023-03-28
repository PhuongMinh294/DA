using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DA.Context;

namespace DA.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DAEntities db = new DAEntities();
        public ActionResult Index()
        {
            var lstCategory = db.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = db.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}