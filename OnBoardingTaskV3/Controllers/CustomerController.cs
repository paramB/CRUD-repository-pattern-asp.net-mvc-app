using OnBoardingTaskV3.DAL;
using OnBoardingTaskV3.Models;
using System.Web.Mvc;

namespace OnBoardingTaskV3.Controllers
{
    public class CustomerController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CustomerController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomerList()
        {
            var custList = unitOfWork.CustRepo.GetAllRecords();            
            return Json(custList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCustomer(Customer cust)
        {            
            var customer = unitOfWork.CustRepo.AddRecord(cust);
            unitOfWork.Save();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult UpdateCustomer(Customer cust)
        {
            unitOfWork.CustRepo.UpdateRecord(cust);            
            return Json(unitOfWork.Save());
        }

        public JsonResult DeleteCustomer(int Id)
        {
            var customer = unitOfWork.CustRepo.DeleteRecord(Id);
            unitOfWork.Save();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
    }
}