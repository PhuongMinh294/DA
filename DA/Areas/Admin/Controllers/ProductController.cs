using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DA.Context;
using DA.Models;
using Microsoft.Owin.Security.Provider;
using static DA.Models.Common;

namespace DA.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DAEntities db = new DAEntities();
        // GET: Admin/Product
        public ActionResult Index(string SearchString)

        {
            
         
            var lstProduct = db.Products.ToList();
            return View(lstProduct);
        }

        public ActionResult Details(int id)
        {
            var objProduct = db.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Common objCommon= new Common();
            //Lấy dữ liệu danh mục dưới db
            var lstCat = db.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //lấy dữ liệu loại hoa dưới db
            var lstBrand = db.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);

            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");


            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giàm giá";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);

            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/item")));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    db.Products.Add(objProduct);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
            
        }

    }
}