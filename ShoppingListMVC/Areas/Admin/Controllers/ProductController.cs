using DataAccess.Repositories.InterfaceRepos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;

namespace ShoppingListMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitofWork context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> AllProducts = _context.Product.GetAll();
            return View(AllProducts);
        }
      
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _context.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),               
            };


            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _context.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (productVM.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (productVM.Product.Id == 0)
                {
                    _context.Product.Add(productVM.Product);
                    TempData["Success"] = "Product created successufly!";
                }
                else
                {
                    _context.Product.Update(productVM.Product);
                    TempData["Success"] = "Product updated successufly!";
                }

                _context.Save();

                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchKey)
        {
            if (searchKey == null)
            {
                IEnumerable<Product> AllProducts = _context.Product.GetAll();
                return View(AllProducts);
            }
            else
            {
                IEnumerable<Product> SearchedProducts = _context.Product.GetAll(a=>a.Name==searchKey);
                return View(SearchedProducts);
            }          
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _context.Product.GetAll(includeProperties: "Category");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _context.Product.GetFirstOrDefault(u => u.Id == id);

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _context.Product.Remove(productToBeDeleted);
            _context.Save();

            return Json(new { success = true, message = "Deleted successfuly" });

        }
        #endregion
    }
}
