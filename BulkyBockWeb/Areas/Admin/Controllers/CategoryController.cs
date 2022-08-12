using BulkyBock.DataAccess.Data;
using BulkyBock.DataAccess.Repository.IRepository;
using BulkyBock.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBockWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Category> ObjectCategoryList = _unitOfWork.Category.GetAll();
            return View(ObjectCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder Can't match exactly the same Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = " Category Created Successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = " Category Edited Successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = " Category Deleted Successfuly";
            return RedirectToAction("Index");
        }
    }
}
