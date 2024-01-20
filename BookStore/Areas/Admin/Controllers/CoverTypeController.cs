using BookStore.DataAccess;
using BookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        // Requesting object from bulider of Program.cs 
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //Convert or Obtain catergory list
            //var objCategoryList = _db.Categories.ToList();

            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFormDb = _db.Categories.Find(id);
            var coverTypeFormDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (coverTypeFormDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFormDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Delete
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFormDb = _db.Categories.Find(id);
            var coverTypeFormDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (coverTypeFormDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFormDbFirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Remove(obj); 
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
