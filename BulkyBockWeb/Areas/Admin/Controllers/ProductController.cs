using BulkyBock.DataAccess.Data;
using BulkyBock.DataAccess.Repository.IRepository;
using BulkyBock.Models;
using BulkyBock.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBockWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,Value = u.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewBag.CoverTypeList = CoverTypeList;
                return View(productVM);
            }
            else
            {

            }
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileSteams = new FileStream(Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(fileSteams);
                    }
                    obj.Product.ImgUrl = @"\images\products" + fileName + extension;
                }
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = " Product Created Successfuly";
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
            var ProductFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (ProductFromDbFirst == null)
            {
                return NotFound();
            }
            return View(ProductFromDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = " Product Deleted Successfuly";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(IncludeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }
    }
}
