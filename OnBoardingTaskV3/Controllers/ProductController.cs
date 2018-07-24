using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnBoardingTaskV3.Models;
using System.Data.Entity;

namespace OnBoardingTaskV3.Controllers
{
    public class ProductController : Controller
    {
        StoreManagementEntities db = new StoreManagementEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProductList()
        {
            var pro = db.Products.Select(x => new
            {
                Id = x.Id,
                PName = x.Name,
                Price = x.Price
            }).ToList();
            return Json(pro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(Product pro)
        {
            db.Products.Add(pro);
            return Json(db.SaveChanges());
        }

        public JsonResult UpdateProduct(Product pro)
        {
            db.Entry(pro).State = EntityState.Modified;
            return Json(db.SaveChanges());
        }

        public JsonResult DeleteProduct(int Id)
        {
            Product pro = db.Products.Find(Id);
            db.Products.Remove(pro);
            return Json(db.SaveChanges());
        }
    }
}