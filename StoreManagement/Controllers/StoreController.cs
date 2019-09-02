using System.Linq;
using System.Web.Mvc;
using StoreManagement.DAL;
using StoreManagement.Models;

namespace StoreManagement.Controllers
{
    public class StoreController : Controller
    {
        private IUnitOfWork unitOfWork;

        public StoreController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStoreList()
        {
            var storeList = unitOfWork.StoreRepo.GetAllRecords();
            return Json(storeList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddStore(Store store)
        {
            var st = unitOfWork.StoreRepo.AddRecord(store);
            unitOfWork.Save();
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateStore(Store store)
        {
            unitOfWork.StoreRepo.UpdateRecord(store);
            unitOfWork.Save();
            return Json("Store updated successfully", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStore(int Id)
        {
            var store = unitOfWork.StoreRepo.DeleteRecord(Id);
            unitOfWork.Save();
            return Json(store, JsonRequestBehavior.AllowGet);
        }
    }
}