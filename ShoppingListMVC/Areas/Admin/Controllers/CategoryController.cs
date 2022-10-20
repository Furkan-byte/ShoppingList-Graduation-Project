using DataAccess.Repositories.InterfaceRepos;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ShoppingListMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _context;
        public CategoryController(IUnitofWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> AllCategories = _context.Category.GetAll();
            return View(AllCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {         
            if (ModelState.IsValid)
            {
                _context.Category.Add(category);
                _context.Save();
                TempData["Success"] = "Category created successufly!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDb = _context.Category.GetFirstOrDefault(u => u.Id == id);
            return View(CategoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            
            if (ModelState.IsValid)
            {
                _context.Category.Update(category);
                _context.Save();
                TempData["Success"] = "Category updated successufly!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Category.GetFirstOrDefault(u => u.Id == id);
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var categoryItem = _context.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }
            _context.Category.Remove(categoryItem);
            _context.Save();
            TempData["Success"] = "Category deleted successufly!";
            return RedirectToAction("Index");

        }
    }
}
