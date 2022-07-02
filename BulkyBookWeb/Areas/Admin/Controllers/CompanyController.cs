using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;       
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company companyObj = new();
       
            if (id == 0 || id == null)
            {
                //Create company
                //ViewBag.CategoryList = categoryList;
                //ViewData["coverTypeList"] = coverTypeList;
                return View(companyObj);

            }
            else
            {
                //Update company
                companyObj = _unitOfWork.company.GetFirstOrDeafult(u => u.Id == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    _unitOfWork.company.Add(companyObj);
                    TempData["success"] = " Create Company is successfully";
                }
                else
                {
                    _unitOfWork.company.Update(companyObj);
                    TempData["success"] = " Update Company is successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(companyObj);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allCompaine= _unitOfWork.company.GetAll();
            return Json(new { data = allCompaine });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyObj = _unitOfWork.company.GetFirstOrDeafult(u => u.Id == id);
            // var coverType = _db.CoverType.FirstOrDefault(c => c.Id == id);
            //var coverType = _db.CoverType.SingleOrDefault(c => c.Id == id);
            if (companyObj == null)
            {
                return Json(new { success = false, message = "Error While deleting" });
            }
            _unitOfWork.company.Remove(companyObj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful !" });
        }
        #endregion
    }
}
