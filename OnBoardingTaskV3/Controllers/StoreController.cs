using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnBoardingTaskV3.Models;

namespace OnBoardingTaskV3.Controllers
{
    public class StoreController : Controller
    {
        StoreManagementEntities db = new StoreManagementEntities();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStoreList()
        {
            var store = db.Stores.Select(x => new
            {
                Id = x.Id,
                SName = x.Name,
                SAddress = x.Address
            }).ToList();
            return Json(store, JsonRequestBehavior.AllowGet);
        }
    }
}