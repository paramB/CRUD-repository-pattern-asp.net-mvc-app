using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnBoardingTaskV3.Models;
using System.Data.Entity;

namespace OnBoardingTaskV3.Controllers
{
    public class CustomerController : Controller
    {
        StoreManagementEntities db = new StoreManagementEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomerList()
        {
            // using LAMBDA syntax
            var customers = db.Customers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Address = x.Address,
            }).ToList();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCustomer(Customer cust)
        {
            db.Customers.Add(cust);
            return Json(db.SaveChanges());
        }

        public JsonResult UpdateCustomer(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            return Json(db.SaveChanges());
        }

        public JsonResult DeleteCustomer(int Id)
        {
            Customer cust = db.Customers.Find(Id);
            db.Customers.Remove(cust);
            return Json(db.SaveChanges());
        }
    }
}