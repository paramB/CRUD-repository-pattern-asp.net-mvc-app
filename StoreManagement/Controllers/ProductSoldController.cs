using System.Web.Mvc;
using StoreManagement.Models;
using StoreManagement.DAL;

namespace StoreManagement.Controllers
{
    public class ProductSoldController : Controller
    {
        private IUnitOfWork unitOfWork;

        public ProductSoldController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSalesList()
        {
            var saleList = unitOfWork.SaleRepo.GetAllRecords();
            return Json(saleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSale(ProductSold psold)
        {               
            unitOfWork.SaleRepo.AddRecord(psold);          
            return Json(unitOfWork.Save());
        }

        public JsonResult UpdateSale(ProductSold psold)
        {
            unitOfWork.SaleRepo.UpdateRecord(psold);         
            return Json(unitOfWork.Save());
        }

        public JsonResult DeleteSale(int Id)
        {
            var del = unitOfWork.SaleRepo.DeleteRecord(Id);
            unitOfWork.Save();
            return Json(del, JsonRequestBehavior.AllowGet);
        }
    }
}