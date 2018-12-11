using System.Web.Mvc;
using OnBoardingTaskV3.Models;
using OnBoardingTaskV3.DAL;

namespace OnBoardingTaskV3.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProductList()
        {
            var pro = unitOfWork.ProdRepo.GetAllRecords();
            return Json(pro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(Product pro)
        {
            var product = unitOfWork.ProdRepo.AddRecord(pro);
            unitOfWork.Save();
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProduct(Product pro)
        {
            unitOfWork.ProdRepo.UpdateRecord(pro);
            unitOfWork.Save();
            return Json("Product updated successfully", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProduct(int Id)
        {
            var product = unitOfWork.ProdRepo.DeleteRecord(Id);
            unitOfWork.Save();
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}