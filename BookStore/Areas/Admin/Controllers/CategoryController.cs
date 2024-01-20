using BookStore.DataAccess;
using BookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // Requesting object from bulider of Program.cs 
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //Convert or Obtain catergory list
            //var objCategoryList = _db.Categories.ToList();

            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DispalyOrder.ToString())
            {
                //Key (name/displayOrder          Message
                ModelState.AddModelError("CustomError", "Display Order can't be same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created successfully";
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
            var categoryFormDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFormDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFormDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DispalyOrder.ToString())
            {
                //Key (name/displayOrder          Message
                ModelState.AddModelError("CustomError", "Display Order can't be same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated successfully";
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
            var categoryFormDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFormDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFormDbFirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Remove(obj); 
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
