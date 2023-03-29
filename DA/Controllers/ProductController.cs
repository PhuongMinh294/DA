using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DA.Context;
using Microsoft.Ajax.Utilities;

namespace DA.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DAEntities db = new DAEntities();
        public ActionResult Detail(int Id)

        {
            var objProduct = db.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        public ActionResult Index(string SearchString) 
        {
            var lstProduct = db.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(lstProduct);
        }
      
    }
}