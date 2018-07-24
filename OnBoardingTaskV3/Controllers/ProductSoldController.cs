using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnBoardingTaskV3.Models;
using System.Data.Entity;

namespace OnBoardingTaskV3.Controllers
{
    public class ProductSoldController : Controller
    {
        StoreManagementEntities db = new StoreManagementEntities();
        // GET: KoProductSold
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSalesList()
        {
            //using Lambda syntax
            var productsold = db.ProductSolds.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Store).Select(x => new
            {
                Id = x.Id,
                Product = x.Product.Name,
                ProductId = x.ProductId,
                Customer = x.Customer.Name,
                CustomerId = x.CustomerId,
                Store = x.Store.Name,
                StoreId = x.StoreId,
                DateSold = x.DateSold,
            }).ToList();
            return Json(productsold, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSale(ProductSold psold)
        {
            db.ProductSolds.Add(psold);
            return Json(db.SaveChanges());
        }
        public JsonResult UpdateSale(ProductSold psold)
        {
            db.Entry(psold).State = EntityState.Modified;
            return Json(db.SaveChanges());
        }
        public JsonResult DeleteSale(int Id)
        {
            ProductSold sale = db.ProductSolds.Find(Id);
            db.ProductSolds.Remove(sale);
            return Json(db.SaveChanges());
        }
    }
}