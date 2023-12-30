using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        // Requesting object from bulider of Program.cs 
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
                _db = db;   
        }

        public IActionResult Index()
        {
            //Convert or Obtain catergory list
            //var objCategoryList = _db.Categories.ToList();
           
            IEnumerable<Category> objCategoryList = _db.Categories;
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
                _db.Categories.Add(obj);
                _db.SaveChanges();
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
            var categoryFormDb = _db.Categories.Find(id);
            //var categoryFormDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            
            if (categoryFormDb == null)
            {
                return NotFound();
            } 
            return View(categoryFormDb);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            var categoryFormDb = _db.Categories.Find(id);
            //var categoryFormDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }              
             _db.Categories.Remove(obj);
             _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
             return RedirectToAction("Index");
            
            
        }

    }
}
