using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objectCoverTypeList = _unitOfWork.coverType.GetAll();

            return View(objectCoverTypeList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.coverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Create CoverType is successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == 0 && id == null)
            {
                return NotFound();
            }
            var coverTypeId = _unitOfWork.coverType.GetFirstOrDeafult(u => u.Id == id);
            if (coverTypeId == null)
            {
                return NotFound();
            }
            return View(coverTypeId);
        }
        [HttpPost]
        public IActionResult Update(CoverType coverType)
        {
   
            if (ModelState.IsValid)
            {
                _unitOfWork.coverType.Update(coverType);
                _unitOfWork.Save();
                TempData["success"] = " Update CoverType is successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 && id == null)
            {
                return NotFound();
            }
            var coverTypeId = _unitOfWork.coverType.GetFirstOrDeafult(u => u.Id == id);
            if (coverTypeId == null)
            {
                return NotFound();
            }
            return View(coverTypeId);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var coverType = _unitOfWork.coverType.GetFirstOrDeafult(u => u.Id == id);
            // var coverType = _db.CoverType.FirstOrDefault(c => c.Id == id);
            //var coverType = _db.CoverType.SingleOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            _unitOfWork.coverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["success"] = "Delete CoverType is successfully";
            return RedirectToAction("Index");
        }
    }
}
